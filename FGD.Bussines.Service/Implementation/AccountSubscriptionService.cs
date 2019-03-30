using FGD.Api;
using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Configuration;
using FGD.Data.Service;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service 
{
    public class AccountSubscriptionService : IAccountSubscriptionService<AccountSubscriptionModelApi<int>, int>
    {
        IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int> _accountSubscriptionRepository;
        IRootFolderService<RootFolderModelBussines<int>, int> _rootFolderService;
        IAccountRepository<AccountModelBussines<int>, int> _accountRepository;
        ISubscriptionRepository<SubscriptionModelApi<int>, int> _subscriptionRepository;
        INotificationRepository<NotificationModelBussines<int>, int> _notificationRepository;

        ISignalRCommunicationService _signalRCommunicationService;

        public AccountSubscriptionService(IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int> accountSubscriptionService,
                    IRootFolderService<RootFolderModelBussines<int>, int> rootFolderService,
                    IAccountRepository<AccountModelBussines<int>, int> accountRepository,
                    ISubscriptionRepository<SubscriptionModelApi<int>, int> subscriptionTypeService,
                    ISignalRCommunicationService signalRCommunicationService,
                    INotificationRepository<NotificationModelBussines<int>, int> notificationRepository
                )
        {

            this._accountSubscriptionRepository = accountSubscriptionService;
            this._rootFolderService = rootFolderService;
            this._accountRepository = accountRepository;
            this._subscriptionRepository = subscriptionTypeService;
            this._signalRCommunicationService = signalRCommunicationService;
            this._notificationRepository = notificationRepository;
        }

        public async Task<AccountSubscriptionModelApi<int>> SubscribeUserAsync(int subscriptionId, IIdentity identity)
        {
            var user = await this._accountRepository.GetByIdAsync(Int32.Parse(identity.Name));

            var rootFolder = await _rootFolderService.CreateRootFolderAsync();
              
            AccountSubscriptionModelBussines<int> model = new AccountSubscriptionModelBussines<int>()
            {
                AccountId = user.Id,
                RootFolderId = rootFolder.Id,
                SubscriptionId = subscriptionId
            };

            NotificationModelApi<int> notification = new NotificationModelApi<int>()
            {
                Descritpion = "You registered and signed in on Common subscription",
                NotificationState = SharedTypes.Enums.NotificationStateEnum.New,
                Title = "Welcome to the service"
            };

            var mapped = AutoMapperConfig.Mapper.Map<NotificationModelBussines<int>>(notification);

            mapped.AccountId = user.Id;

            await this._notificationRepository.CreateAsync(mapped);

            await this._signalRCommunicationService.SendNotificationSignalR(notification, user.Id);

            return AutoMapperConfig.Mapper.Map<AccountSubscriptionModelApi<int>>(
                await this._accountSubscriptionRepository.CreateAsync(model)
                );
        }

        public Task<AccountSubscriptionModelApi<int>> SubscribeUserByIdAsync(int subscriptionId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountSubscriptionModelApi<int>> GetByUserIdAsync(int userId)
        {
            var accSub = await this._accountSubscriptionRepository.GetByUserIdAsync(userId);

            return AutoMapperConfig.Mapper.Map<AccountSubscriptionModelApi<int>>(
                accSub
                );
        }

        public async Task<AccountSubscriptionModelApi<int>> SubscribeUserByIdDefaultAsync(int userId)
        {
            var sub = await _subscriptionRepository.GetByTitleAsync("Free");

            var rootFolder = await _rootFolderService.CreateRootFolderAsync();

            AccountSubscriptionModelBussines<int> model = new AccountSubscriptionModelBussines<int>
            {
                AccountId = userId,
                IsActive = true,
                TakenSpace = 0, 
                RootFolderId = rootFolder.Id,
                SubscriptionId = sub.Id
            };

            NotificationModelBussines<int> notification = new NotificationModelBussines<int>()
            {
                Descritpion = "You registered and signed in on Common subscription",
                NotificationState = SharedTypes.Enums.NotificationStateEnum.New,
                Title = "Welcome to the service",
                AccountId = userId
            };

            await this._notificationRepository.CreateAsync(notification);

            return AutoMapperConfig.Mapper.Map<AccountSubscriptionModelApi<int>>(
               await this._accountSubscriptionRepository.CreateAsync(model)
               ); 
        }
    }
}
