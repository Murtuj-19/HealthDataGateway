using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthDataGateway.Services.Interfaces;
using HealthDataGateway.Data.Models;
using System.Threading.Tasks;

namespace HealthDataGateway.Web.Pages.TargetHospital
{
    public class ViewRequestModel : PageModel
    {
        private readonly ITargetHospitalService _targetHospitalService;
        private readonly IEncryptionService _encryptionService;

        public ViewRequestModel(
            ITargetHospitalService targetHospitalService,
            IEncryptionService encryptionService)
        {
            _targetHospitalService = targetHospitalService;
            _encryptionService = encryptionService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public IncomingTransferRequest IncomingRequest { get; set; }
        public string DecryptedData { get; set; }

        [BindProperty]
        public string Decision { get; set; }

        [BindProperty]
        public string Remarks { get; set; }

        public async Task OnGetAsync()
        {
            IncomingRequest = await _targetHospitalService.GetIncomingRequestByIdAsync(Id);

            if (IncomingRequest?.ConnectorRequest?.EncryptedPayload != null)
            {
                DecryptedData = _encryptionService.Decrypt(IncomingRequest.ConnectorRequest.EncryptedPayload);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            await _targetHospitalService.RespondToRequestAsync(Id, Decision, Remarks);

            TempData["SuccessMessage"] = $"Transfer request {Decision.ToLower()} successfully!";
            return RedirectToPage("./Index");
        }
    }
}