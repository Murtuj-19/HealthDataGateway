using HealthDataGateway.Data;
using HealthDataGateway.Data.Models;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Services.Constants;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthDataGateway.Services.Implementation
{
    public class ConnectorService : IConnectorService
    {
        private readonly ApplicationDbContext _context;

        public ConnectorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConnectorRequest>> GetAllConnectorRequestsAsync()
        {
            return await _context.ConnectorRequests
                .Include(c => c.TransferRequest)
                    .ThenInclude(t => t.SourceHospital)
                .Include(c => c.TransferRequest)
                    .ThenInclude(t => t.TargetHospital)
                .Include(c => c.TransferRequest)
                    .ThenInclude(t => t.Patient)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<ConnectorRequest?> GetConnectorRequestByIdAsync(int connectorRequestId)
        {
            return await _context.ConnectorRequests
                .Include(c => c.TransferRequest)
                    .ThenInclude(t => t.SourceHospital)
                .Include(c => c.TransferRequest)
                    .ThenInclude(t => t.TargetHospital)
                .Include(c => c.TransferRequest)
                    .ThenInclude(t => t.Patient)
                .FirstOrDefaultAsync(c => c.ConnectorRequestId == connectorRequestId);
        }

        public async Task<bool> ProcessConnectorRequestAsync(int connectorRequestId)
        {
            var connectorRequest = await _context.ConnectorRequests
                .Include(c => c.TransferRequest)
                .FirstOrDefaultAsync(c => c.ConnectorRequestId == connectorRequestId);

            if (connectorRequest == null || connectorRequest.Status != Statuses.Connector.Processing)
                return false;

            // Simulate validation and processing
            connectorRequest.Status = Statuses.Connector.Delivered;
            await _context.SaveChangesAsync();

            // Log activity
            _context.ActivityLogs.Add(new ActivityLog
            {
                EntityName = "ConnectorRequest",
                EntityId = connectorRequestId,
                Action = "PROCESSED",
                PerformedBy = "CONNECTOR_GATEWAY",
                LoggedAt = System.DateTime.Now
            });
            await _context.SaveChangesAsync();

            // After processing, automatically deliver to target hospital (create incoming request)
            if (connectorRequest.TransferRequest != null)
            {
                var targetHospitalId = connectorRequest.TransferRequest.TargetHospitalId;
                // Call delivery which will create incoming request and update statuses/logs
                await DeliverToTargetHospitalAsync(connectorRequestId, targetHospitalId);
            }

            return true;
        }

        public async Task<bool> DeliverToTargetHospitalAsync(int connectorRequestId, int targetHospitalId)
        {
            var connectorRequest = await _context.ConnectorRequests
                .Include(c => c.TransferRequest)
                .FirstOrDefaultAsync(c => c.ConnectorRequestId == connectorRequestId);

            if (connectorRequest == null)
                return false;

            // Create incoming transfer request for target hospital
            var incomingRequest = new IncomingTransferRequest
            {
                ConnectorRequestId = connectorRequestId,
                SourceHospitalId = connectorRequest.TransferRequest!.SourceHospitalId,
                TargetHospitalId = targetHospitalId,
                IncomingStatus = Statuses.Incoming.Pending,
                ReceivedAt = System.DateTime.Now
            };

            _context.IncomingTransferRequests.Add(incomingRequest);

            // Update connector request status
            connectorRequest.Status = Statuses.Connector.Delivered;

            // Update transfer request status
            connectorRequest.TransferRequest.Status = Statuses.Transfer.Sent;

            await _context.SaveChangesAsync();

            // Log activity
            _context.ActivityLogs.Add(new ActivityLog
            {
                EntityName = "IncomingTransferRequest",
                EntityId = incomingRequest.IncomingRequestId,
                Action = "DELIVERED",
                PerformedBy = "CONNECTOR_GATEWAY",
                LoggedAt = System.DateTime.Now
            });
            await _context.SaveChangesAsync();

            return true;
        }
    }
}