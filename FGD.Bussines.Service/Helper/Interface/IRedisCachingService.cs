using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service 
{
    public interface IRedisCachingService
    {
        Task<T> GetItemAsync<T>(string key);

        Task<T> PutItemAsync<T>(string key, T item, DistributedCacheEntryOptions options);

        Task RemoveItemAsync(string key);
    }
}
