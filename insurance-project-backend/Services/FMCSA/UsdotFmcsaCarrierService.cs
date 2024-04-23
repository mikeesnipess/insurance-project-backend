using insurance_project_backend.Models.FMCSA;
using insurance_project_backend.Services.FMCSA;
using System.Text.Json;

public class UsdotFmcsaCarrierService : IUsdotFmcsaCarrierService
{
    private readonly HttpClient _httpClient;
    private readonly UsdotConfig _config;

    public UsdotFmcsaCarrierService(HttpClient httpClient, UsdotConfig config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    public async Task<CarrierInfoResponse> GetDataByUsdotNumber(string usdotNumber)
    {
        string requestUri = $"{_config.URL}/{usdotNumber}?webKey={_config.WebKey}";
        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is an error code.

            var jsonResponse = await response.Content.ReadFromJsonAsync<CarrierInfoResponse>();
            if (jsonResponse == null)
                throw new InvalidOperationException("Received null response from the API");

            return jsonResponse;
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Error retrieving data for USDOT number {usdotNumber}: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new ApplicationException($"Invalid JSON received for USDOT number {usdotNumber}: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An unexpected error occurred: {ex.Message}", ex);
        }
    }

    public async Task<CarrierInfoResponse> GetDataByCompanyName(string companyName)
    {
        try
        {
            string requestUri = $"{_config.URL}/name/{companyName}?webKey={_config.WebKey}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadFromJsonAsync<CarrierInfoResponse>();
            if (jsonResponse == null)
                throw new InvalidOperationException("Received null response from the API");

            return jsonResponse;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<CarrierInfoResponse> GetDataByMcMXNumber(string mcMxNumber)
    {
        try
        {
            string requestUri = $"{_config.URL}/docket-number/{mcMxNumber}?webKey={_config.WebKey}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is an error code.

            var jsonResponse = await response.Content.ReadFromJsonAsync<CarrierInfoResponse>();
            if (jsonResponse == null)
                throw new InvalidOperationException("Received null response from the API");

            return jsonResponse;
        }
        catch (Exception ex)
        {
            throw;
        }
    }


}
