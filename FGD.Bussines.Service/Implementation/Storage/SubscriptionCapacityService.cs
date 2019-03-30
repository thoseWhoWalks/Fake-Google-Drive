using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Configuration;
using FGD.Data.Service; 

namespace FGD.Bussines.Service
{
    public class SubscriptionCapacityService : ISubscriptionCapacityService
    {
        private IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>,int> _accountSubscriptionRepository;

        SemaphoreSlim a = new SemaphoreSlim(1);

        private ISubscriptionRepository<SubscriptionModelApi<int>, int> _subscriptionRepository;

        private ISignalRCommunicationService _signalRCommunicationService;

        public SubscriptionCapacityService(IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int> accountSubscriptionRepository,
            ISubscriptionRepository<SubscriptionModelApi<int>, int> subscriptionRepository,
            ISignalRCommunicationService signalRCommunicationService
            )
        {
            this._accountSubscriptionRepository = accountSubscriptionRepository;
            this._signalRCommunicationService = signalRCommunicationService;
            this._subscriptionRepository = subscriptionRepository;
        } 
        public async Task<AccountSubscriptionModelBussines<int>> DecreaseTakenSpaceAsync(int kbs, int userId)
        { 
            var accSub = await this._accountSubscriptionRepository.GetByUserIdAsync(userId);

                accSub.TakenSpace -= kbs;

                var updatedAccSub = await this._accountSubscriptionRepository.UpdateAsync(accSub.RootFolderId, accSub);

                await this._signalRCommunicationService.SendSubscriptionUpdateSignalR(
                        AutoMapperConfig.Mapper.Map<AccountSubscriptionModelApi<int>>(updatedAccSub),
                    userId);

             
            return updatedAccSub;
        }
         
        public async Task<AccountSubscriptionModelBussines<int>> IncreaseTakenSpace(int kbs, int userId)
        {
            var accSub = await this._accountSubscriptionRepository.GetByUserIdAsync(userId);

            accSub.TakenSpace += kbs;

            var updatedAccSub = await this._accountSubscriptionRepository.UpdateAsync(accSub.RootFolderId,accSub);

            await this._signalRCommunicationService.SendSubscriptionUpdateSignalR(
                    AutoMapperConfig.Mapper.Map<AccountSubscriptionModelApi<int>>(updatedAccSub),
                userId);

            return updatedAccSub;
        }

        public async Task<bool> IsFreeForDataPack(long bytes, int userId)
        {
            var accSub = await this._accountSubscriptionRepository.GetByUserIdAsync(userId);

            var sub = await this._subscriptionRepository.GetByIdAsync(accSub.SubscriptionId);

            return (bytes.BytesToKilobytes() + accSub.TakenSpace).KilobytesToGigabytes() < sub.TotalSpace;
        }
         
    }
}
