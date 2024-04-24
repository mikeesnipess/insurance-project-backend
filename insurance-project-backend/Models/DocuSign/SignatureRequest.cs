namespace insurance_project_backend.Models.DocuSign
{
    public class SignatureRequest
    {
        public string AccountId { get; set; }
        public string TemplateId { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientName { get; set; }
    }
}
