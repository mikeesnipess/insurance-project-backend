using insurance_project_backend.Models.Drivers;
using insurance_project_backend.Models.FMCSA;

namespace insurance_project_backend.Services.Drivers
{
    public interface IDriverDetailsService
    {
        Task<bool> ProcessDriverDetails(DriverDetailsModel driverDetails);
    }
}
