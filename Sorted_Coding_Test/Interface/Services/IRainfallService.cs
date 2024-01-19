using Sorted_Coding_Test.Model;

namespace Sorted_Coding_Test.Interface.Services
{
    public interface IRainfallService
    {
        public Task<RainfallReadingResponse> GetRainfallReadings(int stationId, int count);
    }
}
