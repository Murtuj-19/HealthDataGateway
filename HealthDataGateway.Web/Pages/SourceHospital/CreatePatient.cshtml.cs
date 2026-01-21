using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Data.Models;
using System.Threading.Tasks;

namespace HealthDataGateway.Web.Pages.SourceHospital
{
    public class CreatePatientModel : PageModel
    {
        private readonly IPatientService _patientService;

        public CreatePatientModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [BindProperty]
        public Patient Patient { get; set; }

        [BindProperty]
        public string DiagnosisText { get; set; }

        public void OnGet()
        {
            Patient = new Patient { HospitalId = 1 };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Patient.HospitalId = 1;
            var createdPatient = await _patientService.CreatePatientAsync(Patient);

            if (!string.IsNullOrWhiteSpace(DiagnosisText))
            {
                await _patientService.AddDiagnosisAsync(new Diagnosis
                {
                    PatientId = createdPatient.PatientId,
                    DiagnosisText = DiagnosisText
                });
            }

            TempData["SuccessMessage"] = "Patient created successfully!";
            return RedirectToPage("./Index");
        }
    }
}