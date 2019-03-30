using FGD.Api.Model;
using System;
using FGD.Data.Service;
using System.Threading.Tasks;
using FGD.Bussines.Service.Util;
using Microsoft.Extensions.Caching.Distributed;

namespace FGD.Bussines.Service
{
    public class SubscriptionService : ISubscriptionService<SubscriptionModelApi<int>, int>
    {
        private ISubscriptionRepository<SubscriptionModelApi<int>,int> _subscriptionRepository;

        private IRedisCachingService _redisCachingService;

        public SubscriptionService(
            ISubscriptionRepository<SubscriptionModelApi<int>, int> subscriptionRepository,
            IRedisCachingService redisCachingService
            )
        {
            this._subscriptionRepository = subscriptionRepository;
            this._redisCachingService = redisCachingService;
        }

        public async Task<SubscriptionModelApi<int>> GetByIdAsync(int Id)
        {

            var sub = await this._redisCachingService.GetItemAsync<SubscriptionModelApi<int>>(
              RedisCachingKeysUtil.GET_SUBSCRIPTION_BY_ID_KEY + Id
              );

            if (sub == null)
            {
                sub = await this._subscriptionRepository.GetByIdAsync(Id);

                await this._redisCachingService.PutItemAsync<SubscriptionModelApi<int>>(
                       RedisCachingKeysUtil.GET_SUBSCRIPTION_BY_ID_KEY + Id,
                         sub,
                         new DistributedCacheEntryOptions()
                         {
                             AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(14)
                         }
                     );
            }

            return sub;
        }
    }
}
