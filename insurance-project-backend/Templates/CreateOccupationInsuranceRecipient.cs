using insurance_project_backend.Models.DocuSign;
using System.Linq;
using System.Text;

namespace insurance_project_backend.Templates
{
    public class CreateOccupationInsuranceRecipient
    {
        private readonly PdfService _pdfService;

        public CreateOccupationInsuranceRecipient(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public byte[] GenerateRecipient(DocuSignModel docuSignModel)
        {
            var phyCities = string.Join(", ", docuSignModel.CompanyDetails.Content.Select(x => x.Carrier.PhyCity));
            var phyStates = string.Join(", ", docuSignModel.CompanyDetails.Content.Select(x => x.Carrier.PhyState));
            var phyZipcodes = string.Join(", ", docuSignModel.CompanyDetails.Content.Select(x => x.Carrier.PhyZipcode));

            var htmlContent =
                "<!DOCTYPE html>\n" +
                "<html>\n" +
                "    <head>\n" +
                "        <meta charset=\"UTF-8\">\n" +
                "        <title>Occupational Insurance Recipient Form</title>\n" +
                "        <style>\n" +
                "            body { font-family: Arial, sans-serif; margin: 20px; }\n" +
                "            .form-container { max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ccc; border-radius: 5px; }\n" +
                "            .form-group { margin-bottom: 15px; }\n" +
                "            .form-group label { display: block; margin-bottom: 5px; font-weight: bold; }\n" +
                "            .form-group input, .form-group textarea { width: 100%; padding: 8px; box-sizing: border-box; border: 1px solid #ccc; border-radius: 4px; }\n" +
                "        </style>\n" +
                "    </head>\n" +
                "    <body>\n" +
                "        <div class=\"form-container\">\n" +
                "            <h1>Occupational Insurance Recipient Form</h1>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"recipientName\">Recipient Name:</label>\n" +
                "                <input type=\"text\" id=\"recipientName\" name=\"recipientName\" value=\"" + docuSignModel.DriverDetails.FirstName + " " + docuSignModel.DriverDetails.LastName + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"employeeId\">Employee ID:</label>\n" +
                "                <input type=\"text\" id=\"employeeId\" name=\"employeeId\" value=\"" + docuSignModel.DriverDetails.CdiNumber + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"dob\">Date of Birth:</label>\n" +
                "                <input type=\"text\" id=\"dob\" name=\"dob\" value=\"" + docuSignModel.DriverDetails.BirthDay + " " + docuSignModel.DriverDetails.BirthMonth + " " + docuSignModel.DriverDetails.BirthYear + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"ssn\">Social Security Number:</label>\n" +
                "                <input type=\"text\" id=\"ssn\" name=\"ssn\" value=\"" + docuSignModel.DriverDetails.SsnLastFourDigits + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"address\">Address:</label>\n" +
                "                <input type=\"text\" id=\"address\" name=\"address\" value=\"" + docuSignModel.DriverDetails.PrimaryAddress + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"city\">City:</label>\n" +
                "                <input type=\"text\" id=\"city\" name=\"city\" value=\"" + phyCities + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"state\">State:</label>\n" +
                "                <input type=\"text\" id=\"state\" name=\"state\" value=\"" + phyStates + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"zip\">ZIP Code:</label>\n" +
                "                <input type=\"text\" id=\"zip\" name=\"zip\" value=\"" + phyZipcodes + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"phone\">Phone Number:</label>\n" +
                "                <input type=\"text\" id=\"phone\" name=\"phone\" value=\"" + docuSignModel.DriverDetails.MobilePhone + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"email\">Email Address:</label>\n" +
                "                <input type=\"email\" id=\"email\" name=\"email\" value=\"" + docuSignModel.DriverDetails.EmailAddress + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"injuryDate\">Date of Injury:</label>\n" +
                "                <input type=\"date\" id=\"injuryDate\" name=\"injuryDate\" value=\"" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "\">\n" +
                "            </div>\n" +
                "            <div class=\"form-group\">\n" +
                "                <label for=\"insuranceCompany\">Insurance Company:</label>\n" +
                "                <input type=\"text\" id=\"insuranceCompany\" name=\"insuranceCompany\" value=\"Occupation Insurance Company\">\n" +
                "            </div>\n" +
                "        </div>\n" +
                "    </body>\n" +
                "</html>";

            return _pdfService.ConvertHtmlToPdf(htmlContent);
        }
    }
}
