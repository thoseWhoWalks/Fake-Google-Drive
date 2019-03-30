using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Bussines.Service.Util;
using FGD.Configuration;
using FGD.Data.Service;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service 
{
    public class NotificationService : INotificationService<NotificationModelApi<int>, int>
    {
        private INotificationRepository<NotificationModelBussines<int>,int> _notificationRepository;

        private IRedisCachingService _redisCachingService;

        public NotificationService(INotificationRepository<NotificationModelBussines<int>, int> notificationRepository,
            ISignalRCommunicationService signalRCommunicationService,
            IRedisCachingService redisCachingService
            )    
        {
            this._notificationRepository = notificationRepository;
            this._redisCachingService = redisCachingService;
        }

        public async Task<ICollection<NotificationModelApi<int>>> GetAllByUserId(int Id)
        {

            var notes = await this._redisCachingService.GetItemAsync<ICollection<NotificationModelBussines<int>>>(
               RedisCachingKeysUtil.GET_NOTIFICATIONS_BY_USER_ID_KEY + Id
               );

            if (notes == null)
            {
                notes = await this._notificationRepository.GetAllByUserId(Id);

                await this._redisCachingService.PutItemAsync<ICollection<NotificationModelBussines<int>>>(
                         RedisCachingKeysUtil.GET_ACCOUNT_BY_EMAIL_KEY + Id,
                         notes,
                         new DistributedCacheEntryOptions()
                         {
                             AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3),
                             SlidingExpiration = TimeSpan.FromDays(1)
                         }
                     );
            }
           
            return AutoMapperConfig.Mapper.Map<List<NotificationModelApi<int>>>(notes);

        }

        public async Task<NotificationModelApi<int>> UpdateState(int Id)
        {
            var model = await this._notificationRepository.GetByIdAsync(Id);

            model.NotificationState = SharedTypes.Enums.NotificationStateEnum.Read;

            await this._redisCachingService.RemoveItemAsync(RedisCachingKeysUtil.GET_NOTIFICATIONS_BY_USER_ID_KEY);

            return AutoMapperConfig.Mapper.Map<NotificationModelApi<int>>(
                await this._notificationRepository.UpdateAsync(Id, AutoMapperConfig.Mapper.Map<NotificationModelBussines<int>>(model))
                );
        }
         
    }
}
