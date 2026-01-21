using HealthDataGateway.Data;
using HealthDataGateway.Data.Models;
using HealthDataGateway.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthDataGateway.Services.Implementation
{
    public class TransferService : ITransferService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncryptionService _encryptionService;

        public TransferService(ApplicationDbContext context, IEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        public async Task<TransferRequest> CreateTransferRequestAsync(int sourceHospitalId, int patientId, int targetHospitalId)
        {
            var transferRequest = new TransferRequest
            {
                SourceHospitalId = sourceHospitalId,
                PatientId = patientId,
                TargetHospitalId = targetHospitalId,
                Status = "CREATED",
                CreatedAt = DateTime.Now
            };

            _context.TransferRequests.Add(transferRequest);
            await _context.SaveChangesAsync();

            // Log activity
            _context.ActivityLogs.Add(new ActivityLog
            {
                EntityName = "TransferRequest",
                EntityId = transferRequest.TransferRequestId,
                Action = "CREATED",
                PerformedBy = "SOURCE_HOSPITAL",
                LoggedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return transferRequest;
        }

        public async Task<bool> PushToConnectorAsync(int transferRequestId)
        {
            var transferRequest = await _context.TransferRequests
                .Include(t => t.Patient)
                    .ThenInclude(p => p.Diagnoses)
                .FirstOrDefaultAsync(t => t.TransferRequestId == transferRequestId);

            if (transferRequest == null || transferRequest.Status != "CREATED")
                return false;

            // Create payload with patient data
            var payload = new
            {
                transferRequest.PatientId,
                transferRequest.Patient!.FullName,
                transferRequest.Patient.DOB,
                transferRequest.Patient.Gender,
                transferRequest.Patient.LocalPatientId,
                Diagnoses = transferRequest.Patient.Diagnoses.Select(d => new
                {
                    d.DiagnosisText,
                    d.DiagnosedAt
                }).ToList()
            };

            string jsonPayload = JsonSerializer.Serialize(payload);
            byte[] encryptedPayload = _encryptionService.Encrypt(jsonPayload);

            // Create connector request
            var connectorRequest = new ConnectorRequest
            {
                TransferRequestId = transferRequestId,
                EncryptedPayload = encryptedPayload,
                EncryptionType = "AES-256",
                AuthType = "OAuth2",
                IsFHIRCompliant = true,
                Status = "PROCESSING",
                CreatedAt = DateTime.Now
            };

            _context.ConnectorRequests.Add(connectorRequest);

            // Update transfer request status
            transferRequest.Status = "QUEUED";

            await _context.SaveChangesAsync();

            // Log activity
            _context.ActivityLogs.Add(new ActivityLog
            {
                EntityName = "ConnectorRequest",
                EntityId = connectorRequest.ConnectorRequestId,
                Action = "QUEUED",
                PerformedBy = "SOURCE_HOSPITAL",
                LoggedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TransferRequest>> GetTransferRequestsByHospitalAsync(int hospitalId)
        {
            return await _context.TransferRequests
                .Include(t => t.Patient)
                .Include(t => t.TargetHospital)
                .Where(t => t.SourceHospitalId == hospitalId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<TransferRequest?> GetTransferRequestByIdAsync(int transferRequestId)
        {
            return await _context.TransferRequests
                .Include(t => t.Patient)
                    .ThenInclude(p => p.Diagnoses)
                .Include(t => t.TargetHospital)
                .Include(t => t.SourceHospital)
                .FirstOrDefaultAsync(t => t.TransferRequestId == transferRequestId);
        }

        public async Task<bool> CancelTransferRequestAsync(int transferRequestId)
        {
            var transferRequest = await _context.TransferRequests
                .FirstOrDefaultAsync(t => t.TransferRequestId == transferRequestId);

            if (transferRequest == null)
                return false;

            transferRequest.Status = "CANCELLED";
            await _context.SaveChangesAsync();

            return true;
        }
    }
}