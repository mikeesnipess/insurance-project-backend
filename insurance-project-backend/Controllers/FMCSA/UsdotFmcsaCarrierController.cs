using insurance_project_backend.Services.FMCSA;
using Microsoft.AspNetCore.Mvc;

namespace insurance_project_backend.Controllers.FMCSA
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsdotFmcsaCarrierController : ControllerBase
    {
        private readonly IUsdotFmcsaCarrierService _carrierService;

        public UsdotFmcsaCarrierController(IUsdotFmcsaCarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        // GET api/UsdotFmcsaCarrier/usdot/12345
        [HttpGet("usdot/{usdotNumber}")]
        public async Task<IActionResult> GetCarrierInfo(string usdotNumber)
        {
            try
            {
                var result = await _carrierService.GetDataByUsdotNumber(usdotNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/UsdotFmcsaCarrier/mcmx/67890
        [HttpGet("mcmx/{mcMxNumber}")]
        public async Task<IActionResult> GetMcMxInfo(string mcMxNumber)
        {
            try
            {
                var result = await _carrierService.GetDataByMcMXNumber(mcMxNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/UsdotFmcsaCarrier/company/SomeCompanyName
        [HttpGet("company/{companyName}")]
        public async Task<IActionResult> GetDataCompanyName(string companyName)
        {
            try
            {
                var result = await _carrierService.GetDataByCompanyName(companyName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
