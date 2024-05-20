using insurance_project_backend.Models.FMCSA;

namespace insurance_project_backend.Services.Company
{
    public class CompanyDetailsService : ICompanyDetailsService
    {
        public async Task<bool> ProcessCompanyDetails(CarrierInfoResponseModel carrierInfo)
        {
            try
            {
                // Iterate through the Content list
                foreach (var content in carrierInfo.Content)
                {
                    // Access the Carrier properties directly
                    var dotNumber = content.Carrier.DotNumber;
                    var legalName = content.Carrier.LegalName;
                    var allowedToOperate = content.Carrier.AllowedToOperate;

                    Console.WriteLine($"Received company: DOT Number = {dotNumber}, Legal Name = {legalName}, Allowed to Operate = {allowedToOperate}");
                }

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
