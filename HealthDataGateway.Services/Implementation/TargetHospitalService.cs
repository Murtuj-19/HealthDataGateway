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

            if (incomingRequest == null || incomingRequest.IncomingStatus != Statuses.Incoming.Pending)
                return false;

            // Begin transaction to keep updates atomic
            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                // Update incoming request status
                incomingRequest.IncomingStatus = decision;

                // Update related transfer and connector statuses if available
                var connectorRequest = incomingRequest.ConnectorRequest;
                var transferRequest = connectorRequest?.TransferRequest;

                if (transferRequest != null)
                {
                    if (decision == Statuses.Incoming.Accepted)
                    {
                        transferRequest.Status = Statuses.Transfer.Accepted;
                    }
                    else if (decision == Statuses.Incoming.Rejected)
                    {
                        transferRequest.Status = Statuses.Transfer.Rejected;
                    }

                    // Optionally mark as Sent when accepted (if business requires SENT vs ACCEPTED)
                    // transferRequest.Status = decision == Statuses.Incoming.Accepted ? Statuses.Transfer.Sent : transferRequest.Status;

                    _context.ActivityLogs.Add(new ActivityLog
                    {
                        EntityName = "TransferRequest",
                        EntityId = transferRequest.TransferRequestId,
                        Action = decision,
                        PerformedBy = $"TARGET_HOSPITAL_{incomingRequest.TargetHospitalId}",
                        LoggedAt = DateTime.Now
                    });
                }

                if (connectorRequest != null)
                {
                    // mark connector request delivered/processed
                    connectorRequest.Status = Statuses.Connector.Delivered;

                    _context.ActivityLogs.Add(new ActivityLog
                    {
                        EntityName = "ConnectorRequest",
                        EntityId = connectorRequest.ConnectorRequestId,
                        Action = decision,
                        PerformedBy = $"TARGET_HOSPITAL_{incomingRequest.TargetHospitalId}",
                        LoggedAt = DateTime.Now
                    });
                }

                // **NEW: If ACCEPTED, update patient's hospital**
                if (decision == Statuses.Incoming.Accepted && incomingRequest.ConnectorRequest?.TransferRequest?.Patient != null)
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

                // Persist all changes
                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                // Log acknowledgement activity (after commit to ensure acknowledgement id)
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
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}