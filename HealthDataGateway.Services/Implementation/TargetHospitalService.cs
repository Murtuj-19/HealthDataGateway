using HealthDataGateway.Data;
using HealthDataGateway.Data.Models;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Services.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthDataGateway.Services.Implementation
{
    public class TargetHospitalService : ITargetHospitalService
    {
        private readonly ApplicationDbContext _context;

        public TargetHospitalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IncomingTransferRequest>> GetIncomingRequestsAsync(int hospitalId)
        {
            return await _context.IncomingTransferRequests
                .Include(i => i.SourceHospital)
                .Include(i => i.ConnectorRequest)
                    .ThenInclude(c => c.TransferRequest)
                        .ThenInclude(t => t.Patient)
                .Where(i => i.TargetHospitalId == hospitalId)
                .OrderByDescending(i => i.ReceivedAt)
                .ToListAsync();
        }

        public async Task<IncomingTransferRequest?> GetIncomingRequestByIdAsync(int incomingRequestId)
        {
            return await _context.IncomingTransferRequests
                .Include(i => i.SourceHospital)
                .Include(i => i.TargetHospital)
                .Include(i => i.ConnectorRequest)
                    .ThenInclude(c => c.TransferRequest)
                        .ThenInclude(t => t.Patient)
                            .ThenInclude(p => p.Diagnoses)
                .FirstOrDefaultAsync(i => i.IncomingRequestId == incomingRequestId);
        }

        public async Task<bool> RespondToRequestAsync(int incomingRequestId, string decision, string remarks)
        {
            var incomingRequest = await _context.IncomingTransferRequests
                .Include(i => i.ConnectorRequest)
                    .ThenInclude(c => c.TransferRequest)
                        .ThenInclude(t => t.Patient)
                .FirstOrDefaultAsync(i => i.IncomingRequestId == incomingRequestId);

            if (incomingRequest == null || incomingRequest.IncomingStatus != "PENDING")
                return false;

            // Update incoming request status
            incomingRequest.IncomingStatus = decision;

            // **NEW: If ACCEPTED, update patient's hospital**
            if (decision == "ACCEPTED" && incomingRequest.ConnectorRequest?.TransferRequest?.Patient != null)
            {
                var patient = incomingRequest.ConnectorRequest.TransferRequest.Patient;
                patient.HospitalId = incomingRequest.TargetHospitalId;

                // Log the transfer completion
                _context.ActivityLogs.Add(new ActivityLog
                {
                    EntityName = "Patient",
                    EntityId = patient.PatientId,
                    Action = "TRANSFERRED",
                    PerformedBy = $"TARGET_HOSPITAL_{incomingRequest.TargetHospitalId}",
                    LoggedAt = DateTime.Now
                });
            }

            // Create acknowledgement
            var acknowledgement = new Acknowledgement
            {
                IncomingRequestId = incomingRequestId,
                Decision = decision,
                Remarks = remarks,
                RespondedAt = DateTime.Now
            };

            _context.Acknowledgements.Add(acknowledgement);
            await _context.SaveChangesAsync();

            // Log activity
            _context.ActivityLogs.Add(new ActivityLog
            {
                EntityName = "Acknowledgement",
                EntityId = acknowledgement.AcknowledgementId,
                Action = decision,
                PerformedBy = "TARGET_HOSPITAL",
                LoggedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return true;
        }
    }
}