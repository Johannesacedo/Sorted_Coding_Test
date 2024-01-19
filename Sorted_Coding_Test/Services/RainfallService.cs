using Newtonsoft.Json;
using Sorted_Coding_Test.Interface.Services;
using Sorted_Coding_Test.Model;
using Microsoft.Extensions.Logging;

namespace Sorted_Coding_Test.Services
{
    public class RainfallService : IRainfallService
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<RainfallService> _logger;

        public RainfallService(HttpClient httpClient, ILogger<RainfallService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<RainfallReadingResponse> GetRainfallReadings(int stationId, int count)
        {
            try
            {
                var requestUri = new UriBuilder("http://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings")
                {
                    Query = $"count={count}"
                }.Uri;

                // Make an HTTP request to the external API
                var response = await _httpClient.GetAsync(requestUri);

                // Ensure a successful response
                response.EnsureSuccessStatusCode();

                // Read and parse the JSON response
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RainfallReadingResponse>(json);

                return result;
            }
            catch (HttpRequestException ex)
            {
                // Log the exception using ILogger
                _logger.LogError(ex, $"Error calling external service for stationId {stationId}");

                // Re-throw the exception
                throw;
            }
        }
    }
}
