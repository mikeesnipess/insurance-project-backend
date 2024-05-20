using AutoMapper;
using insurance_project_backend.Models.FMCSA;
using insurance_project_backend.Services.Company;
using Microsoft.AspNetCore.Mvc;

namespace insurance_project_backend.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDetailsController : ControllerBase
    {
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly IMapper _mapper;

        public CompanyDetailsController(ICompanyDetailsService companyDetailsService, IMapper mapper)
        {
            _companyDetailsService = companyDetailsService;
            _mapper = mapper;
        }

        [HttpPost("companyDetails")]
        public async Task<IActionResult> SubmitDriverDetails([FromBody] CarrierInfoResponse companyDetailsModel)
        {
            if (companyDetailsModel == null)
            {
                return BadRequest(new { message = "Company details are null." });
            }

            try
            {
                var companyDetails = _mapper.Map<CarrierInfoResponseModel>(companyDetailsModel);
                bool result = await _companyDetailsService.ProcessCompanyDetails(companyDetails);
                if (result)
                {
                    return Ok(new { message = "Company details processed successfully." });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to process company details." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
