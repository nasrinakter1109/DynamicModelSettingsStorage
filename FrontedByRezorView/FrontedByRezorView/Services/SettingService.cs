using FrontedByRezorView.Repositories;
using System.Text.Json;
using System.Text;
using FrontedByRezorView.Models;

namespace FrontedByRezorView.Services
{
    public class SettingService : ISetting
    {
        private readonly HttpClient _client;

        public SettingService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Settings>> GetSettingsByCategoryAsync(string category)
        {
            // Use string interpolation to insert the category
            var response = await _client.GetAsync($"https://localhost:7092/api/Settings/category/{category}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                // Options for deserialization
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Handle case insensitivity
                };

                var settings = JsonSerializer.Deserialize<List<Settings>>(json, options);

                return settings ?? new List<Settings>(); // Ensure it's not null
            }

            // Log or handle the error
            // You can log the error here, for example:
            // _logger.LogError($"Error fetching settings for category '{category}': {response.StatusCode}");

            return new List<Settings>(); // Return an empty list instead of null
        }


        public async Task SaveOrUpdateSettingAsync(string key, string value)
        {
            // Validate key and value
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }

            var settings = new Dictionary<string, string>
            {
                { key, value ?? string.Empty } // Ensure value is not null; if it is, use an empty string
            };

            var json = JsonSerializer.Serialize(settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Adjust URL to the correct API endpoint
            var response = await _client.PostAsync("https://localhost:7092/api/Settings", content);

            // Ensure successful status code
            response.EnsureSuccessStatusCode();
        }


    }
}
