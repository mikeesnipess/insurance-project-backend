using Microsoft.AspNetCore.Mvc;
using insurance_project_backend.Models.DocuSign;
using insurance_project_backend.Templates;
using System.Threading.Tasks;

namespace insurance_project_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratePdfController : ControllerBase
    {
        private readonly CreateOccupationInsuranceRecipient _createOccupationInsuranceRecipient;

        public GeneratePdfController(CreateOccupationInsuranceRecipient createOccupationInsuranceRecipient)
        {
            _createOccupationInsuranceRecipient = createOccupationInsuranceRecipient;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GeneratePdf([FromBody] DocuSignModel docuSignModel)
        {
            if (docuSignModel == null)
            {
                return BadRequest("Invalid data.");
            }

            var pdfBytes = _createOccupationInsuranceRecipient.GenerateRecipient(docuSignModel);

            return File(pdfBytes, "application/pdf", "OccupationInsuranceRecipient.pdf");
        }
    }
}
