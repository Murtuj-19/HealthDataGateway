using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HealthDataGateway.Web.Pages.Connector
{
    public class IndexModel : PageModel
    {
        private readonly IConnectorService _connectorService;

        public IndexModel(IConnectorService connectorService)
        {
            _connectorService = connectorService;
        }

        public List<ConnectorRequest> ConnectorRequests { get; set; }
        public int ProcessingCount { get; set; }
        public int DeliveredCount { get; set; }
        public int FailedCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public async Task OnGetAsync()
        {
            var all = await _connectorService.GetAllConnectorRequestsAsync();

            ProcessingCount = all.Count(c => c.Status == "PROCESSING");
            DeliveredCount = all.Count(c => c.Status == "DELIVERED");
            FailedCount = all.Count(c => c.Status == "FAILED");

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                ConnectorRequests = all.Where(c => c.Status == Filter.ToUpperInvariant()).ToList();
            }
            else
            {
                ConnectorRequests = all;
            }
        }

        public async Task<IActionResult> OnPostDeliverAsync(int connectorRequestId, int targetHospitalId)
        {
            await _connectorService.DeliverToTargetHospitalAsync(connectorRequestId, targetHospitalId);
            TempData["SuccessMessage"] = "Request delivered to target hospital successfully!";
            return RedirectToPage();
        }
    }
}