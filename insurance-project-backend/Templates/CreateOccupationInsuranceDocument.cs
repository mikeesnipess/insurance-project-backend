using System;
using System.Linq;
using System.Text;
using insurance_project_backend.Models.DocuSign;

namespace insurance_project_backend.Templates
{
    public class CreateOccupationInsuranceDocument
    {
        private readonly PdfService _pdfService;

        public CreateOccupationInsuranceDocument(PdfService pdfService)
        {
            _pdfService = pdfService;
        }
        public byte[] GenerateDocument(DocuSignModel docuSignModel)
        {
            var companyDetails = docuSignModel.CompanyDetails.Content.FirstOrDefault();
            var legalName = companyDetails?.Carrier?.LegalName ?? "N/A"; // Default to "N/A" if null
            var companyCode = companyDetails?.Carrier?.DotNumber?.ToString() ?? "N/A"; // Default to "N/A" if null
            var state = companyDetails?.Carrier?.PhyState ?? "N/A"; // Default to "N/A" if null
            var address = companyDetails?.Carrier?.PhyStreet ?? "N/A"; // Default to "N/A" if null

            var htmlContent =
                "<!DOCTYPE html>\n" +
                "<html>\n" +
                "    <head>\n" +
                "        <meta charset=\"UTF-8\">\n" +
                "    </head>\n" +
                "    <body style=\"font-family:sans-serif;margin-left:2em;\">\n" +
                "        <h1 style=\"font-family: 'Trebuchet MS', Helvetica, sans-serif;\n" +
                "            color: black;margin-bottom: 0;\">EKORA Insurance</h1>\n" +
                "        <h2 style=\"font-family: 'Trebuchet MS', Helvetica, sans-serif;\n" +
                "          margin-top: 0px;margin-bottom: 3.5em;font-size: 1em;\n" +
                "          color: darkblue;\">Occupation Insurance Request</h2>\n" +
                "        <h4>Requested by " + docuSignModel.DriverDetails.FirstName + " " + docuSignModel.DriverDetails.LastName + "</h4>\n" +
                "        <p style=\"margin-top:0em; margin-bottom:0em;\">Email: " + docuSignModel.DriverDetails.EmailAddress + "</p>\n" +
                "        <p style=\"margin-top:0em; margin-bottom:0em;\">Copy to: " + docuSignModel.DriverDetails.EmailAddress + "</p>\n" +
                "        <p style=\"margin-top:3em;\">\n" +
                "            Congratulations on your new occupation! Please provide the following information for your occupation insurance request:\n" +
                "        </p>\n" +
                "        <p style=\"margin-top:1em;\">\n" +
                "            • Your company name: " + legalName + "\n" +
                "        </p>\n" +
                "        <p style=\"margin-top:1em;\">\n" +
                "            • Your driver name: " + docuSignModel.DriverDetails.FirstName + " " + docuSignModel.DriverDetails.LastName + "\n" +
                "        </p>\n" +
                "        <p style=\"margin-top:1em;\">\n" +
                "            • Your company code: " + companyCode + "\n" +
                "        </p>\n" +
                "        <p style=\"margin-top:1em;\">\n" +
                "            • State: " + state + "\n" +
                "        </p>\n" +
                "        <p style=\"margin-top:1em;\">\n" +
                "            • Address: " + address + "\n" +
                "        </p>\n" +
                "        <p style=\"margin-top:1em;\">\n" +
                "            • Birth Date: " + docuSignModel.DriverDetails.BirthDay + " " + docuSignModel.DriverDetails.BirthMonth + " " + docuSignModel.DriverDetails.BirthYear + "\n" +
                "        </p>\n" +
                "        <p style=\"margin-top:1em;\">\n" +
                "            • SSN: " + docuSignModel.DriverDetails.SsnLastFourDigits + "\n" +
                "        </p>\n" +
                "        <p style=\"margin-top:1em;\">\n" +
                "            • Mobile Phone: " + docuSignModel.DriverDetails.MobilePhone + "\n" +
                "        </p>\n" +
                "        <h3 style=\"margin-top:3em;\">Agreed and signed: <span style=\"color:white;\">**signature_1**/</span></h3>\n" +
                "        <p>\n" +
                "            Please review the above information carefully. By signing, you confirm that the details provided are accurate and complete.\n" +
                "        </p>\n" +
                "        <p>\n" +
                "            Leverage agile frameworks to provide a robust synopsis for high level overviews. Iterative approaches to corporate strategy foster collaborative thinking to further the overall value proposition. Organically grow the holistic world view of disruptive innovation via workplace diversity and empowerment.\n" +
                "        </p>\n" +
                "        <p>\n" +
                "            Bring to the table win-win survival strategies to ensure proactive domination. At the end of the day, going forward, a new normal that has evolved from generation X is on the runway heading towards a streamlined cloud solution. User generated content in real-time will have multiple touchpoints for offshoring.\n" +
                "        </p>\n" +
                "        <p>\n" +
                "            Collaboratively administrate empowered markets via plug-and-play networks. Dynamically procrastinate B2C users after installed base benefits. Dramatically visualize customer directed convergence without revolutionary ROI.\n" +
                "        </p>\n" +
                "    </body>\n" +
                "</html>";

            return _pdfService.ConvertHtmlToPdf(htmlContent);
        }
    }
}
