// <copyright file="SigningViaEmail.cs" company="DocuSign">
// Copyright (c) DocuSign. All rights reserved.
// </copyright>

namespace ESignature.Examples
{
    using DocuSign.eSign.Api;
    using DocuSign.eSign.Client;
    using DocuSign.eSign.Model;
    using insurance_project_backend.Models.DocuSign;
    using insurance_project_backend.Models.Drivers;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class SigningViaEmail
    {
        /// <summary>
        /// Creates an envelope that would include two documents and add a signer and cc recipients to be notified via email
        /// </summary>
        /// <param name="signerEmail">Email address for the signer</param>
        /// <param name="signerName">Full name of the signer</param>
        /// <param name="ccEmail">Email address for the cc recipient</param>
        /// <param name="ccName">Name of the cc recipient</param>
        /// <param name="accessToken">Access Token for API call (OAuth)</param>
        /// <param name="basePath">BasePath for API calls (URI)</param>
        /// <param name="accountId">The DocuSign Account ID (GUID or short version) for which the APIs call would be made</param>
        /// <param name="docPdf">String of bytes representing the document (pdf)</param>
        /// <param name="docDocx">String of bytes representing the Word document (docx)</param>
        /// <param name="envStatus">Status to set the envelope to</param>
        /// <returns>EnvelopeId for the new envelope</returns>
        public static string SendEnvelopeViaEmail(DocuSignModel docuSignModel, string signerEmail, string signerName, string ccEmail, string ccName, string accessToken, string basePath, string accountId, string docDocx, string docPdf, string envStatus)
        {
            //ds-snippet-start:eSign2Step3
            EnvelopeDefinition env = MakeEnvelope(docuSignModel, signerEmail, signerName, ccEmail, ccName, docDocx, docPdf, envStatus);
            var docuSignClient = new DocuSignClient(basePath);
            docuSignClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);

            EnvelopesApi envelopesApi = new EnvelopesApi(docuSignClient);
            EnvelopeSummary results = envelopesApi.CreateEnvelope(accountId, env);
            return results.EnvelopeId;
            //ds-snippet-end:eSign2Step
        }

        public static EnvelopeDefinition MakeEnvelope(DocuSignModel docuSignModel, string signerEmail, string signerName, string ccEmail, string ccName, string docDocx, string docPdf, string envStatus)
        {
            // Data for this method
            // signerEmail
            // signerName
            // ccEmail
            // ccName
            // Config.docDocx
            // Config.docPdf
            // RequestItemsService.Status -- the envelope status ('created' or 'sent')

            // document 1 (html) has tag **signature_1**
            // document 2 (docx) has tag /sn1/
            // document 3 (pdf) has tag /sn1/
            //
            // The envelope has two recipients.
            // recipient 1 - signer
            // recipient 2 - cc
            // The envelope will be sent first to the signer.
            // After it is signed, a copy is sent to the cc person.
            // read files from a local directory
            // The reads could raise an exception if the file is not available!
            string doc2DocxBytes = Convert.ToBase64String(System.IO.File.ReadAllBytes(docDocx));
            string doc3PdfBytes = Convert.ToBase64String(System.IO.File.ReadAllBytes(docPdf));

            //ds-snippet-start:eSign2Step2
            EnvelopeDefinition env = new EnvelopeDefinition();
            env.EmailSubject = "Please sign this document set";

            // Create document objects, one per document
            Document doc1 = new Document();
            string b64 = Convert.ToBase64String(Document1(docuSignModel, signerEmail, signerName, ccEmail, ccName));
            doc1.DocumentBase64 = b64;
            doc1.Name = "Order acknowledgement"; // can be different from actual file name
            doc1.FileExtension = "html"; // Source data format. Signed docs are always pdf.
            doc1.DocumentId = "1"; // a label used to reference the doc
            Document doc2 = new Document
            {
                DocumentBase64 = doc2DocxBytes,
                Name = "Battle Plan", // can be different from actual file name
                FileExtension = "docx",
                DocumentId = "2",
            };
            Document doc3 = new Document
            {
                DocumentBase64 = doc3PdfBytes,
                Name = "Lorem Ipsum", // can be different from actual file name
                FileExtension = "pdf",
                DocumentId = "3",
            };

            // The order in the docs array determines the order in the envelope
            env.Documents = new List<Document> { doc1, doc2, doc3 };

            // create a signer recipient to sign the document, identified by name and email
            // We're setting the parameters via the object creation
            Signer signer1 = new Signer
            {
                Email = signerEmail,
                Name = signerName,
                RecipientId = "1",
                RoutingOrder = "1",
            };

            // routingOrder (lower means earlier) determines the order of deliveries
            // to the recipients. Parallel routing order is supported by using the
            // same integer as the order for two or more recipients.

            // create a cc recipient to receive a copy of the documents, identified by name and email
            // We're setting the parameters via setters
            CarbonCopy cc1 = new CarbonCopy
            {
                Email = ccEmail,
                Name = ccName,
                RecipientId = "2",
                RoutingOrder = "2",
            };

            // Create signHere fields (also known as tabs) on the documents,
            // We're using anchor (autoPlace) positioning
            //
            // The DocuSign platform searches throughout your envelope's
            // documents for matching anchor strings. So the
            // signHere2 tab will be used in both document 2 and 3 since they
            // use the same anchor string for their "signer 1" tabs.
            SignHere signHere1 = new SignHere
            {
                AnchorString = "**signature_1**",
                AnchorUnits = "pixels",
                AnchorYOffset = "10",
                AnchorXOffset = "20",
            };

            SignHere signHere2 = new SignHere
            {
                AnchorString = "/sn1/",
                AnchorUnits = "pixels",
                AnchorYOffset = "10",
                AnchorXOffset = "20",
            };

            // Tabs are set per recipient / signer
            Tabs signer1Tabs = new Tabs
            {
                SignHereTabs = new List<SignHere> { signHere1, signHere2 },
            };
            signer1.Tabs = signer1Tabs;

            // Add the recipients to the envelope object
            Recipients recipients = new Recipients
            {
                Signers = new List<Signer> { signer1 },
                CarbonCopies = new List<CarbonCopy> { cc1 },
            };
            env.Recipients = recipients;

            // Request that the envelope be sent by setting |status| to "sent".
            // To request that the envelope be created as a draft, set to "created"
            env.Status = envStatus;

            return env;
            //ds-snippet-end:eSign2Step2
        }

        public static byte[] Document1(DocuSignModel docuSignModel, string signerEmail, string signerName, string ccEmail, string ccName)
        {
            var companyDetails = docuSignModel.CompanyDetails.Content.FirstOrDefault();
            var legalName = companyDetails?.Carrier?.LegalName ?? "N/A"; // Default to "N/A" if null
            var companyCode = companyDetails?.Carrier?.DotNumber?.ToString() ?? "N/A"; // Default to "N/A" if null
            var state = companyDetails?.Carrier?.PhyState ?? "N/A"; // Default to "N/A" if null
            var address = companyDetails?.Carrier?.PhyStreet ?? "N/A"; // Default to "N/A" if null

            return Encoding.UTF8.GetBytes(
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
                "        <h4>Requested by " + signerName + "</h4>\n" +
                "        <p style=\"margin-top:0em; margin-bottom:0em;\">Email: " + docuSignModel.DriverDetails.EmailAddress + "</p>\n" +
                "        <p style=\"margin-top:0em; margin-bottom:0em;\">Copy to: " + ccName + ", " + ccEmail + "</p>\n" +
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
                "</html>");
        }

    }
}
