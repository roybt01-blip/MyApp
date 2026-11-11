using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApp
{
    public class GarminService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "GCDC_37e562f7-9848-4cc9-994d-768e9d76bd0f.3cb5a307-84b9-4868-a89c-6ccc17e93f15.kOKoSSBSnrNHG1DpX+WN/rXjrS0FCuvvY5hQ5nfq6Zo.1503586690"; // Replace with your actual key
        private string _bearerToken;
        private DateTime _tokenExpiration = DateTime.MinValue;

        public GarminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 1. Authenticate and get the Bearer token
        private async Task EnsureAuthenticatedAsync()
        {
            if (!string.IsNullOrEmpty(_bearerToken) && DateTime.Now < _tokenExpiration)
                return;

            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://personal.connectapi.garmin.com/personalapitoken-service/personal-api/personal-api-key/token");

            request.Headers.Add("Authorization", $"ApiKey {_apiKey}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _bearerToken = (await response.Content.ReadAsStringAsync()).Trim();
                _tokenExpiration = DateTime.Now.AddHours(23); // Safe buffer below 24-hr expiry
            }
            else
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                throw new Exception($"Authentication failed ({response.StatusCode}): {errorDetails}");
            }
        }

        // 2. Fetch Sleep Data
        public async Task<JsonElement?> GetSleepDataAsync(string dateString) // Format: "YYYY-MM-DD"
        {
            await EnsureAuthenticatedAsync();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://personal.connectapi.garmin.com/sleep-service/sleep/dailySleep?calendarDate={dateString}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<JsonElement>();
            }

            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to fetch sleep data ({response.StatusCode}): {errorDetails}");
        }

        // 3. Fetch User Summary (HRV, Stress, Body Battery, etc.)
        public async Task<JsonElement?> GetUserSummaryAsync(string dateString)
        {
            await EnsureAuthenticatedAsync();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://personal.connectapi.garmin.com/usersummary-service/usersummary/daily/{dateString}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<JsonElement>();
            }

            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to fetch summary data ({response.StatusCode}): {errorDetails}");
        }
    }
}