using System.Net.Http;
using System.Text.Json;
using ClinicaBlazor.Models;
using Microsoft.Extensions.Options;

namespace ClinicaBlazor.Services
{
    public class ReCaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly ReCaptchaSettings _settings;

        public ReCaptchaService(HttpClient httpClient, IOptions<ReCaptchaSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<bool> VerificarTokenAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            var form = new Dictionary<string, string>
            {
                { "secret", _settings.SecretKey },
                { "response", token }
            };

            using var content = new FormUrlEncodedContent(form);
            using var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ReCaptchaVerifyResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.Success == true;
        }

        private class ReCaptchaVerifyResponse
        {
            public bool Success { get; set; }
            public string[]? ErrorCodes { get; set; }
        }
    }
}