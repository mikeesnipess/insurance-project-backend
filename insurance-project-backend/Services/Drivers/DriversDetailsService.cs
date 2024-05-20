using insurance_project_backend.Models.Drivers;

namespace insurance_project_backend.Services.Drivers
{
    public class DriverDetailsService : IDriverDetailsService
    {
        public async Task<bool> ProcessDriverDetails(DriverDetailsModel driverDetails)
        {
            try
            {
                Console.WriteLine($"Received driver details: {driverDetails.FirstName} {driverDetails.LastName}");

                await Task.Delay(100);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
