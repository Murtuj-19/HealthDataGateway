using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthDataGateway.Services.Interfaces
{
    public interface ITargetHospitalService
    {
        Task<List<IncomingTransferRequest>> GetIncomingRequestsAsync(int hospitalId);
        Task<IncomingTransferRequest?> GetIncomingRequestByIdAsync(int incomingRequestId);
        Task<bool> RespondToRequestAsync(int incomingRequestId, string decision, string remarks);
    }
}