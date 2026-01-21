using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task OnGetAsync()
        {
            ConnectorRequests = await _connectorService.GetAllConnectorRequestsAsync();

            ProcessingCount = ConnectorRequests.FindAll(c => c.Status == "PROCESSING").Count;
            DeliveredCount = ConnectorRequests.FindAll(c => c.Status == "DELIVERED").Count;
            FailedCount = ConnectorRequests.FindAll(c => c.Status == "FAILED").Count;
        }

        public async Task<IActionResult> OnPostDeliverAsync(int connectorRequestId, int targetHospitalId)
        {
            await _connectorService.DeliverToTargetHospitalAsync(connectorRequestId, targetHospitalId);
            TempData["SuccessMessage"] = "Request delivered to target hospital successfully!";
            return RedirectToPage();
        }
    }
}