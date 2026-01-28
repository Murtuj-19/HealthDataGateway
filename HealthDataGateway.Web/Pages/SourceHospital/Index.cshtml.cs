using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HealthDataGateway.Web.Pages.SourceHospital
{
    public class IndexModel : PageModel
    {
        private readonly ITransferService _transferService;
        private readonly IPatientService _patientService;

        public IndexModel(ITransferService transferService, IPatientService patientService)
        {
            _transferService = transferService;
            _patientService = patientService;
        }

        public List<Patient> Patients { get; set; }
        public List<TransferRequest> TransferRequests { get; set; }
        public int HospitalId { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        // Counts for the left-side cards
        public int CountCreated { get; set; }
        public int CountQueued { get; set; }
        public int CountSent { get; set; }

        public async Task OnGetAsync()
        {
            Patients = await _patientService.GetPatientsByHospitalAsync(HospitalId);
            var allRequests = await _transferService.GetTransferRequestsByHospitalAsync(HospitalId);

            // compute counts
            CountCreated = allRequests.Count(t => t.Status == "CREATED");
            CountQueued = allRequests.Count(t => t.Status == "QUEUED");
            CountSent = allRequests.Count(t => t.Status == "SENT");

            // apply filter if present
            if (!string.IsNullOrWhiteSpace(Filter))
            {
                TransferRequests = allRequests.Where(t => t.Status == Filter.ToUpperInvariant()).ToList();
            }
            else
            {
                TransferRequests = allRequests;
            }
        }
    }
}

