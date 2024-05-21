using AutoMapper;
using insurance_project_backend.Models.DocuSign;
using insurance_project_backend.Models.FMCSA;
using insurance_project_backend.Services.DocuSign;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DocuSignController : ControllerBase
{
    private readonly DocuSignClientService _docuSignClientService;
    private readonly IMapper _mapper;

    public DocuSignController(DocuSignClientService docuSignClientService, IMapper mapper)
    {
        _docuSignClientService = docuSignClientService;
        _mapper = mapper;
    }

    [HttpPost("SendEnvelope")]
    public IActionResult SendEnvelope([FromBody] DocuSignModel request)
    {
        if (request?.DriverDetails == null || request?.CompanyDetails == null)
        {
            return BadRequest(new { message = "Details are null." });
        }
        try
        {
            var companyDetails = _mapper.Map<CarrierInfoResponseModel>(request.CompanyDetails);

            string result = _docuSignClientService.AuthenticateAndSendEnvelope(request);
            return Ok(new { result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
        }
    }

}
