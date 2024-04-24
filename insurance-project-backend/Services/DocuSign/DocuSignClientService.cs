using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using DocuSign.eSign.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

public class DocuSignClientService
{
    private readonly ApiClient _apiClient;
    private readonly EnvelopesApi _envelopesApi;

    public DocuSignClientService(IConfiguration configuration)
    {
        string integratorKey = configuration["DocuSign:IntegratorKey"];
        string userId = configuration["DocuSign:UserId"];
        string oAuthBasePath = configuration["DocuSign:OAuthBasePath"];
        string host = configuration["DocuSign:Host"];
        string privateKeyText = configuration["DocuSign:PrivateKey"];

        try
        {
            string cleanedPrivateKey = CleanPrivateKey(privateKeyText);
            byte[] privateKeyBytes = Convert.FromBase64String(cleanedPrivateKey); // This line might throw a format exception if the key is still not clean
            _apiClient = new ApiClient(host);
            AuthenticateWithJwt(integratorKey, userId, oAuthBasePath, privateKeyBytes);
            _envelopesApi = new EnvelopesApi(_apiClient);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to initialize DocuSign client: " + ex.Message);
            throw new ApplicationException("Failed to initialize DocuSign client.", ex);
        }
    }


    private string CleanPrivateKey(string privateKey)
    {
        if (string.IsNullOrWhiteSpace(privateKey))
            throw new ApplicationException("Private key is empty");

        // Remove headers and footers if present
        privateKey = privateKey.Replace("-----BEGIN PRIVATE KEY-----", "")
                                .Replace("-----END PRIVATE KEY-----", "");

        // Remove new lines and spaces if any within the key itself
        privateKey = privateKey.Replace("\n", "").Replace("\r", "").Replace(" ", "").Trim();

        // Debug output to verify the cleaned key
        Console.WriteLine("Cleaned Private Key: " + privateKey);

        return privateKey;
    }



    private void AuthenticateWithJwt(string integratorKey, string userId, string oAuthBasePath, byte[] privateKeyBytes)
    {
        try
        {
            OAuth.OAuthToken tokenInfo = _apiClient.RequestJWTUserToken(integratorKey, userId, oAuthBasePath, privateKeyBytes, 1);
            _apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + tokenInfo.access_token);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Authentication failed: " + ex.Message, ex);
        }
    }



public EnvelopeSummary SendEnvelopeFromTemplate(string accountId, string templateId, List<TemplateRole> templateRoles)
    {
        EnvelopeDefinition envelopeDefinition = new EnvelopeDefinition
        {
            TemplateId = templateId,
            TemplateRoles = templateRoles,
            Status = "sent"
        };

        try
        {
            return _envelopesApi.CreateEnvelope(accountId, envelopeDefinition);
        }
        catch (ApiException apiEx)
        {
            Console.WriteLine("DocuSign API Exception: " + apiEx.ErrorCode + ": " + apiEx.Message);
            throw;
        }
    }
}
