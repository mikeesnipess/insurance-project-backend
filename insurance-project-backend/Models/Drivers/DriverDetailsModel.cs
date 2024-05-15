namespace insurance_project_backend.Models.Drivers
{
    public class DriverDetailsModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public int? BirthDay { get; set; }
        public string? BirthMonth { get; set; }
        public int? BirthYear { get; set; }
        public string? SsnLastFourDigits { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobilePhone { get; set; }
        public string? PaidBy { get; set; }
        public string? CdiNumber { get; set; }
        public string? CdlNumber { get; set; }
        public int? IssueDateDay { get; set; }
        public string? IssueDateMonth { get; set; }
        public int? IssueDateYear { get; set; }
        public string? PrimaryAddress { get; set; }
        public string? PrimaryState { get; set; }
        public string? PrimaryPostalCode { get; set; }
        public string? BeneficiaryName { get; set; }
        public string? Relationship { get; set; }
        public string? BeneficiaryPhone { get; set; }
        public string? TypeOfVehicle { get; set; }

        // Parameterless constructor
        public DriverDetailsModel() { }
    }
}
