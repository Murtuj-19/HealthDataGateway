using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.AspNetCore.Mvc;
using System.IO;
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
        public int HospitalId { get; set; } = 2;

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public int PendingCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }

        public async Task OnGetAsync()
        {
            var all = await _targetHospitalService.GetIncomingRequestsAsync(HospitalId);

            PendingCount = all.Count(i => i.IncomingStatus == "PENDING");
            AcceptedCount = all.Count(i => i.IncomingStatus == "ACCEPTED");
            RejectedCount = all.Count(i => i.IncomingStatus == "REJECTED");

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                IncomingRequests = all.Where(i => i.IncomingStatus == Filter.ToUpperInvariant()).ToList();
            }
            else
            {
                IncomingRequests = all;
            }
        }
        public IActionResult OnGetJson()
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", "data", "hospital2.txt");

            if (!System.IO.File.Exists(path))
            {
                return Content("JSON file not found");
            }

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "text/plain");
        }
    }
}
