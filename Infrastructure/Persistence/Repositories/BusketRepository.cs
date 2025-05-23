using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BusketRepository(IConnectionMultiplexer connectionMultiplexer) : IBusketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public Task<bool> DeleteBusketAsync(string id)
            => _database.KeyDeleteAsync(id);

        public async Task<CustomerBusket?> GetBusketByIdAsync(string id)
        {
            var value = await _database.StringGetAsync(id);

            if (value.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<CustomerBusket>(value!);
        }

        public async Task<CustomerBusket?> UpdateBusketAsync(CustomerBusket busket, TimeSpan? timeToLive = null)
        {
            string value = JsonSerializer.Serialize(busket);
            bool isCreatedOrUpdated = await _database.StringSetAsync(busket.Id, value, timeToLive ?? TimeSpan.FromDays(30));

            return isCreatedOrUpdated ? await GetBusketByIdAsync(busket.Id) : null;
        }
    }
}
