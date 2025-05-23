using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class CacheService(ICacheRepository repository) : ICacheService
    {
        public async Task<string?> GetCahcedItem(string key)
            => await repository.GetAsync(key);

        public async Task SetCacheValue(string key, object value, TimeSpan duration)
            => await repository.SetAsync(key, value, duration); 
    }
}
