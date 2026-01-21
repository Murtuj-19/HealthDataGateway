using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthDataGateway.Services.Interfaces
{
    public interface IConnectorService
    {
        Task<List<ConnectorRequest>> GetAllConnectorRequestsAsync();
        Task<ConnectorRequest?> GetConnectorRequestByIdAsync(int connectorRequestId);
        Task<bool> ProcessConnectorRequestAsync(int connectorRequestId);
        Task<bool> DeliverToTargetHospitalAsync(int connectorRequestId, int targetHospitalId);
    }
}