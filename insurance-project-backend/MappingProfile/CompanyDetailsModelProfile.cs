using AutoMapper;
using insurance_project_backend.Models.FMCSA;

namespace insurance_project_backend.MappingProfile
{
    public class CompanyDetailsModelProfile : Profile
    {
        public CompanyDetailsModelProfile()
        {
            CreateMap<CarrierInfoResponse, CarrierInfoResponseModel>();
        }
    }
}
