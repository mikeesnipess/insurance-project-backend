using AutoMapper;
using insurance_project_backend.Models.FMCSA;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CarrierInfoResponseModel, CarrierInfoResponse>();
        CreateMap<ContentModel, Content>();
        CreateMap<LinksModel, Links>();
        CreateMap<LinkModel, Link>();
        CreateMap<CarrierModel, Carrier>();
        CreateMap<CarrierOperationModel, CarrierOperation>();
        CreateMap<CensusTypeIdModel, CensusTypeId>();
    }
}
