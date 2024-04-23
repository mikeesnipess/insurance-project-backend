using insurance_project_backend.Models.FMCSA;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace insurance_project_backend.Services.FMCSA
{
    public interface IUsdotFmcsaCarrierService
    {
        Task<CarrierInfoResponse> GetDataByUsdotNumber(string usdotNumber);
        Task<CarrierInfoResponse> GetDataByMcMXNumber(string mcMxNumber);
        Task<CarrierInfoResponse> GetDataByCompanyName(string companyName);
    }
}
