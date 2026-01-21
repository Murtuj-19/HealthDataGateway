using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthDataGateway.Data;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthDataGateway.Web.Pages.SourceHospital
{
    public class CreateTransferModel : PageModel
    {
        private readonly ITransferService _transferService;
        private readonly IPatientService _patientService;
        private readonly ApplicationDbContext _context;

        public CreateTransferModel(
            ITransferService transferService,
            IPatientService patientService,
            ApplicationDbContext context)
        {
            _transferService = transferService;
            _patientService = patientService;
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int PatientId { get; set; }

        [BindProperty]
        public int TargetHospitalId { get; set; }

        public Patient Patient { get; set; }
        public List<SelectListItem> Hospitals { get; set; }

        public async Task OnGetAsync()
        {
            Patient = await _patientService.GetPatientByIdAsync(PatientId);
            await LoadHospitalsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Patient = await _patientService.GetPatientByIdAsync(PatientId);
                await LoadHospitalsAsync();
                return Page();
            }

            var transferRequest = await _transferService.CreateTransferRequestAsync(1, PatientId, TargetHospitalId);
            await _transferService.PushToConnectorAsync(transferRequest.TransferRequestId);

            TempData["SuccessMessage"] = "Transfer request created and sent to connector!";
            return RedirectToPage("./Index");
        }

        private async Task LoadHospitalsAsync()
        {
            var hospitals = await _context.Hospitals
                .Where(h => h.HospitalId != 1 && h.IsActive)
                .ToListAsync();

            Hospitals = hospitals.Select(h => new SelectListItem
            {
                Value = h.HospitalId.ToString(),
                Text = h.HospitalName
            }).ToList();
        }
    }
}
