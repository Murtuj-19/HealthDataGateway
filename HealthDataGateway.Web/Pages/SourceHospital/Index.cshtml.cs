
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task OnGetAsync()
        {
            Patients = await _patientService.GetPatientsByHospitalAsync(HospitalId);
            TransferRequests = await _transferService.GetTransferRequestsByHospitalAsync(HospitalId);
        }
    }
}

