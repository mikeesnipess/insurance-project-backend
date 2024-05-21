using insurance_project_backend.Models.Drivers;
using insurance_project_backend.Models.FMCSA;

namespace insurance_project_backend.Models.DocuSign
{
    public class DocuSignModel
    {
        public DriverDetailsModel DriverDetails { get; set; }
        public CarrierInfoResponseModel CompanyDetails { get; set; }
    }
}
