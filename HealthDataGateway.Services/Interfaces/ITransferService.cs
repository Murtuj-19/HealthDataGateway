using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthDataGateway.Services.Interfaces
{
    public interface ITransferService
    {
        Task<TransferRequest> CreateTransferRequestAsync(int sourceHospitalId, int patientId, int targetHospitalId);
        Task<bool> PushToConnectorAsync(int transferRequestId);
        Task<List<TransferRequest>> GetTransferRequestsByHospitalAsync(int hospitalId);
        Task<TransferRequest?> GetTransferRequestByIdAsync(int transferRequestId);
        Task<bool> CancelTransferRequestAsync(int transferRequestId);
    }
}