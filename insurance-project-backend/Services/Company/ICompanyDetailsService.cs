using insurance_project_backend.Models.Drivers;
using insurance_project_backend.Models.FMCSA;

namespace insurance_project_backend.Services.Company
{
    public interface ICompanyDetailsService
    {
        Task<bool> ProcessCompanyDetails(CarrierInfoResponseModel carrierInfo);
    }
}
