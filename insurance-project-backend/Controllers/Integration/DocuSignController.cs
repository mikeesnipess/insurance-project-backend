using insurance_project_backend.Services.DocuSign;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class DocuSignController : ControllerBase
{
    private readonly DocuSignClientService _docuSignClientService;

    public DocuSignController(DocuSignClientService docuSignClientService)
    {
        _docuSignClientService = docuSignClientService;
    }

    [HttpPost("SendEnvelope")]
    public IActionResult SendEnvelope()
    {
        _docuSignClientService.AuthenticateAndSendEnvelope();
        return Ok("Envelope sent successfully.");
    }
}
