using DocuSign.CodeExamples.Authentication;
using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using ESignature.Examples;
using insurance_project_backend.Models.DocuSign;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace insurance_project_backend.Services.DocuSign
{
    public class DocuSignClientService
    {
        private readonly IConfiguration _configuration;
        private static readonly string DevCenterPage = "https://developers.docusign.com/platform/auth/consent";

        public DocuSignClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AuthenticateAndSendEnvelope(DocuSignModel docuSignModel)
        {
            var docuSignConfig = _configuration.GetSection("DocuSign");
            var clientId = docuSignConfig["ClientId"];
            var authServer = docuSignConfig["AuthServer"];
            var impersonatedUserId = docuSignConfig["ImpersonatedUserID"];
            var privateKeyFilePath = docuSignConfig["PrivateKeyFile"];

            OAuth.OAuthToken accessToken = null;
            try
            {
                accessToken = JwtAuth.AuthenticateWithJwt(
                    "ESignature",
                    clientId,
                    impersonatedUserId,
                    authServer,
                    File.ReadAllBytes(privateKeyFilePath)
                );
            }
            catch (ApiException apiExp)
            {
                if (apiExp.Message.Contains("consent_required"))
                {
                    RequestConsent(authServer, clientId);
                    return "Consent required. Please grant consent and try again.";
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {apiExp.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                return $"Error: {apiExp.Message}";
            }

            var docuSignClient = new DocuSignClient();
            docuSignClient.SetOAuthBasePath(authServer);
            var userInfo = docuSignClient.GetUserInfo(accessToken.access_token);
            var account = userInfo.Accounts.FirstOrDefault();

            if (account == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No account found");
                Console.ForegroundColor = ConsoleColor.White;
                return "Error: No account found";
            }

            string signerEmail = docuSignModel.DriverDetails.EmailAddress; // Driver Email
            string signerName = docuSignModel.DriverDetails.FirstName + " " + docuSignModel.DriverDetails.LastName; // Driver FirstName LastName
            string ccEmail = "artur.bejenari00@gmail.com"; // CC email
            string ccName = "Artur Bejenari"; // CC Name;
            string docDocx = Path.Combine(@"D:\UNIVER\ANUL 4\Licenta\Site\insurance-project-backend\insurance-project-backend\", "World_Wide_Corp_salary.docx");
            string docPdf = Path.Combine(@"D:\UNIVER\ANUL 4\Licenta\Site\insurance-project-backend\insurance-project-backend\", "World_Wide_Corp_lorem.pdf");

            string envelopeId = SigningViaEmail.SendEnvelopeViaEmail(
                docuSignModel,
                signerEmail,
                signerName,
                ccEmail,
                ccName,
                accessToken.access_token,
                account.BaseUri + "/restapi",
                account.AccountId,
                docDocx,
                docPdf,
                "sent"
            );
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Successfully sent envelope with envelopeId {envelopeId}");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;

            return envelopeId;
        }


        private void RequestConsent(string authServer, string clientId)
        {
            string caret = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "^" : "";
            string url = $"https://{authServer}/oauth/auth?response_type=code{caret}&scope=impersonation%20signature{caret}&client_id={clientId}{caret}&redirect_uri={DevCenterPage}";

            string consentRequiredMessage = $"Consent is required - launching browser (URL is {url})";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                consentRequiredMessage = consentRequiredMessage.Replace(caret, "");
            }

            Console.WriteLine(consentRequiredMessage);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = false });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unable to send envelope; Exiting. Please rerun the console app once consent was provided");
            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit(-1);
        }
    }
}
