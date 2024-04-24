using DocuSign.eSign.Model;
using insurance_project_backend.Models.DocuSign;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class DocuSignController : ControllerBase
{
    private readonly DocuSignClientService _docuSignClientService;

    public DocuSignController(DocuSignClientService docuSignClientService)
    {
        _docuSignClientService = docuSignClientService;
    }

    [HttpPost("sendDocumentUsingTemplate")]
public IActionResult SendDocumentUsingTemplate([FromBody] SignatureRequest request)
{
    try
    {
        var templateRoles = new List<TemplateRole>
        {
            new TemplateRole
            {
                Email = request.RecipientEmail,
                Name = request.RecipientName,
                RoleName = "Signer"  // Ensure this matches the role name specified in your DocuSign template
            }
        };

        var result = _docuSignClientService.SendEnvelopeFromTemplate(request.AccountId, request.TemplateId, templateRoles);
        return Ok($"Envelope sent with ID: {result.EnvelopeId}");
    }
    catch (Exception ex)
    {
        return BadRequest($"An error occurred: {ex.Message}");
    }
}


}
