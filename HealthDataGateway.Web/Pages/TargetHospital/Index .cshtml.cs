using HealthDataGateway.Data.Models;
using HealthDataGateway.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthDataGateway.Web.Pages.TargetHospital
{
   
    public class IndexModel : PageModel
    {
        private readonly ITargetHospitalService _targetHospitalService;

        public IndexModel(ITargetHospitalService targetHospitalService)
        {
            _targetHospitalService = targetHospitalService;
        }

        public List<IncomingTransferRequest> IncomingRequests { get; set; }
        public int PendingCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }
        public int HospitalId { get; set; } = 2;

        public async Task OnGetAsync()
        {
            IncomingRequests = await _targetHospitalService.GetIncomingRequestsAsync(HospitalId);

            PendingCount = IncomingRequests.FindAll(r => r.IncomingStatus == "PENDING").Count;
            AcceptedCount = IncomingRequests.FindAll(r => r.IncomingStatus == "ACCEPTED").Count;
            RejectedCount = IncomingRequests.FindAll(r => r.IncomingStatus == "REJECTED").Count;
        }
    }
}