using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public class RedisCachingService : IRedisCachingService
    {
        private IDistributedCache _distributedCache;

        public RedisCachingService(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            var item = await _distributedCache.GetAsync(key);

            if (item == null)
                return default(T);

            var deserializedItem = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(item));

            return deserializedItem;
        }

        public async Task RemoveItemAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task<T> PutItemAsync<T>(string key, T item, DistributedCacheEntryOptions options)
        {
            var serializedItem = JsonConvert.SerializeObject(item);

            var storedItem = Encoding.UTF8.GetBytes(serializedItem);

            await this._distributedCache.SetAsync(key,storedItem, options);

            return item;
        }
    }
}
