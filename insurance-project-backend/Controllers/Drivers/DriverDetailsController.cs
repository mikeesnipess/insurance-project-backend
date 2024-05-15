using insurance_project_backend.Models.Drivers;
using insurance_project_backend.Services.Drivers;
using Microsoft.AspNetCore.Mvc;

namespace insurance_project_backend.Controllers.Drivers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverDetailsController : ControllerBase
    {
        private readonly IDriverDetailsService _driverDetailsService;

        public DriverDetailsController(IDriverDetailsService driverDetailsService)
        {
            _driverDetailsService = driverDetailsService;
        }

        [HttpPost("drivers")]
        public async Task<IActionResult> SubmitDriverDetails([FromBody] DriverDetailsModel driverDetails)
        {
            if (driverDetails == null)
            {
                return BadRequest(new { message = "Driver details are null." });
            }

            try
            {
                bool result = await _driverDetailsService.ProcessDriverDetails(driverDetails);
                if (result)
                {
                    return Ok(new { message = "Driver details processed successfully." });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to process driver details." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
