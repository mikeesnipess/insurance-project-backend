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
        private readonly CreateOccupationInsuranceDocument _createOccupationInsuranceDocument;

        public GeneratePdfController(CreateOccupationInsuranceRecipient createOccupationInsuranceRecipient, CreateOccupationInsuranceDocument createOccupationInsuranceDocument)
        {
            _createOccupationInsuranceRecipient = createOccupationInsuranceRecipient;
            _createOccupationInsuranceDocument = createOccupationInsuranceDocument;
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

        [HttpPost("generateDocument")]
        public async Task<IActionResult> GenerateDocument([FromBody] DocuSignModel docuSignModel)
        {
            if (docuSignModel == null)
            {
                return BadRequest("Invalid data.");
            }

            var pdfBytes = _createOccupationInsuranceDocument.GenerateDocument(docuSignModel);

            return File(pdfBytes, "application/pdf", "OccupationInsuranceDocument.pdf");
        }
    }
}
