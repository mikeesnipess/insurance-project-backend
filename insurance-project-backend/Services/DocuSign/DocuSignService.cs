using System;
using System.Text;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using DocuSign.eSign.Model;

public class DocuSignService
{
    public static void InitializeDocuSign()
    {
        string IntegratorKey = "e37648cf-f28d-4122-927c-2ac630abce4b";
        string UserId = "5d269c97-ae26-4356-9cf2-c8e339c1d0d3"; // Also known as API Account ID
        string OAuthBasePath = "account-d.docusign.com";
        string privateKey = "MIIEowIBAAKCAQEArZMzvJ/C4zfhfqfqBAym72oRx6CVpKOCjvFIEztEpekrk9l9\r\nrh35mjcgDVpktrTRkFArw7bJkRGcbnnAcJZYBBDk1dmMgGK+67b5BXOy3tWBCqPB\r\nqkKRlfyHgj/k4NDnyGe3mFL+9wWSJPDQ6r9hoTGVbaUmXbFCVWyV+EooQc7OYT8S\r\nw4ju9H6IspikT5IbFJl/idUD9GkRaV/x0j7IfWaCxmcaYN0j5oHiDIsVGvk9pqnq\r\n45WOw4p+L8tZ5DbDZAJwIQccmrD9dU1k1ywu7PojPMI5W3TSKlQ6HN+HmoKVCa5A\r\n2kNXOITnaujg/tM7nDYXhPS1XqBsANYmBkdQBwIDAQABAoIBADiB1RSBsEkXfp4k\r\nnR8vXUNQ9TPXmA/uuUi+zReRsnIO3ER4SB2gnYsyigk+1c3TROwl5XGENs+2+4XN\r\ntjQRBdthApaKCd8cBoqnWR+IwIEh4a4sjJZYsJuzCAyDr4fvCp4oAdGhp2aU1jSX\r\nf3QE1QxMn5h/7OMQm4dC3/niboOpfX+LIl/SVJrSDqCr4S1Pj5NPyGg3ZvBxg4SW\r\n8fJjQLvIwg71O5z+aWKhReREQ3XMXrY8ERZwUgMAfXE7ZSvBGBJB4GXIuto+Ilhx\r\nNDTqhnFBPmcU1qUZnKa4BN8MMbCr3t4YgBg2mRpWyKDFnFaoLYazCqDu13HBT8vb\r\nuLhyxokCgYEA6PU696f3epaxxO8Z9VBzBjUHgtRE+FfnDyYMElbJDFsI8WwAN+3n\r\njA33a1UQU9kOp05i+nixQJxwQCtWekGcaz/xSegsJcjGpj8uMJ3B2M1diS1zxJxP\r\nNiBaaji2f9RbrCEJmjf/whIZCZ4RPYpxKpynnviK6KOKIdlR4f3deUkCgYEAvr5T\r\nepOW678YletoobMbNRFNCJUAEfLVaa9s0vIPOMuN7kZtKkJQ5LEJ+wPKNtCXBAGm\r\nCBdQvq95gdsZQ5hgc8goLdn0l4anN+rsyIilhB+afn6j6J0XXrWP9rfwH5AvznIj\r\nh51LT+HCeuGs5DoRnMR94O6kQ+hU1rS/g8lOTs8CgYARcUMgBfCOujfHQgvUhViH\r\nb3MHhbNAUPyY8sVWni7hgPNzeB2wEl0S7HzJCY52aLsjJchdbqn+53e0JkqMTttS\r\nYHBFk1+y2IyfJ/3iX/2CqSXGvqN7ZiS8LcH3UBXCDnlqsvKQHH9G3aoIQVCTJgmB\r\nIkeerIdiEdSm4imyXRq3GQKBgEJIdU3lN87Yazy9v3BhgxTf3DsdPBcAobTGJzTu\r\ntOdxpPKmOo0y2lV+SA+Yc4600aWsxPuOzppU9A5yCsd4jhvt9DhqwxpcyoMd+gZ9\r\npbbSHKfUbjUJUfOxeEOuC52MR3CgLUSVHnS16RU2kxQEMdaeWXfutdp7Q/AQg3TS\r\nYYn1AoGBAJ1buV065JsC84ddk5xElQpjdplgupysk/GLD94wzX7zltXVext+LYoR\r\nwy0FWDUiygjOpiYPlmgLNzaqHQQzqmCBDYxSwJU5fvdcmLx3POY6HlVfl+hZURS1\r\nETUaCe37T7ht3PunHvpod72vh7vt13rxF2h9Vbk7sitfcZbfsUIX"; // Typically loaded from a secure location
        string host = "https://demo.docusign.net/restapi";

        ApiClient apiClient = new ApiClient(host);

        // Set up JWT authentication
        OAuth.OAuthToken tokenInfo = apiClient.RequestJWTUserToken(IntegratorKey, UserId, OAuthBasePath, Encoding.UTF8.GetBytes(privateKey), 1);

        // Set the authentication header
        apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + tokenInfo.access_token);

        var envelopesApi = new EnvelopesApi(apiClient);
    }
}
