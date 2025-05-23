using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ICacheService
    {
        Task SetCacheValue(string key, object value, TimeSpan duration);
        Task<string?> GetCahcedItem(string key);
    }
}
