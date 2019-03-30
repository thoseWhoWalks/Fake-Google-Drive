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
    public class SharedFolderService : ISharedFolderService<int, SharedFolderModelApi<int>, StoredFolderModelApi<int>>
    {
        private IStoredFolderRepository<StoredFolderModelBussines<int>, int> _storedFolderRepository;

        private ISharedFolderRepository<SharedFolderModelBussines<int>, int> _sharedFolderRepository;

        private IAccountRepository<AccountModelBussines<int>, int> _accountRepository;

        private INotificationRepository<NotificationModelBussines<int>, int> _notificationRepository;

        private ISignalRCommunicationService _signalRCommunicationService;

        private IRedisCachingService _redisCachingService;

        public SharedFolderService(
                IStoredFolderRepository<StoredFolderModelBussines<int>, int> storedFolderRepository,
                ISharedFolderRepository<SharedFolderModelBussines<int>, int> sharedFolderRepository,
                IAccountRepository<AccountModelBussines<int>, int> accountRepository,
                INotificationRepository<NotificationModelBussines<int>, int> notificationRepository,
                ISignalRCommunicationService signalRCommunicationService,
                IRedisCachingService redisCachingService
            )
        {
            this._storedFolderRepository = storedFolderRepository;
            this._sharedFolderRepository = sharedFolderRepository;
            this._accountRepository = accountRepository;
            this._redisCachingService = redisCachingService;
            this._notificationRepository = notificationRepository;
            this._signalRCommunicationService = signalRCommunicationService;
        }

        public async Task<ICollection<StoredFolderModelApi<int>>> GetAllByUserId(int UserId)
        {
            ICollection<SharedFolderModelBussines<int>> sharedFolders = await GetFromCacheOrDb(UserId);

            return await MapToStoredFolders(sharedFolders);

        }

        private async Task<ICollection<SharedFolderModelBussines<int>>> GetFromCacheOrDb(int UserId)
        {
            var sharedFolders = await this._redisCachingService.GetItemAsync<ICollection<SharedFolderModelBussines<int>>>(
                          RedisCachingKeysUtil.GET_SHARED_FOLDERS_BY_USER_ID + UserId
                          );

            if (sharedFolders == null)
            {
                sharedFolders = await this._sharedFolderRepository.GetAllByUserIdAsync(UserId);

                await this._redisCachingService.PutItemAsync(
                        RedisCachingKeysUtil.GET_SHARED_FOLDERS_BY_USER_ID + UserId,
                        sharedFolders,
                        new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(10)
                        }
                        );
            }

            return sharedFolders;
        }

        private async Task<List<StoredFolderModelApi<int>>> MapToStoredFolders(ICollection<SharedFolderModelBussines<int>> sharedFolders)
        {
            var storedFolders = new List<StoredFolderModelApi<int>>();

            foreach (var folder in sharedFolders)
                storedFolders.Add(AutoMapperConfig.Mapper.Map<StoredFolderModelApi<int>>(
                       await this._storedFolderRepository.GetByIdAsync(folder.StoredFolderId)
                    ));

            return storedFolders;
        }

        public async Task<ResponseModel<SharedFolderModelApi<int>>> ShareFolder(SharedFolderModelApi<int> sharingModel)
        {
            var acc = await this._accountRepository.GetByEmailAsync(sharingModel.AccountEmail);

            var resp = new ResponseModel<SharedFolderModelApi<int>>();

            if (acc == null)
            {
                resp.AddError(new Error($"This user not exists"));
                return resp;
            }

            await this._redisCachingService.RemoveItemAsync(RedisCachingKeysUtil.GET_SHARED_FOLDERS_BY_USER_ID + acc.Id);

            var folder = await this._storedFolderRepository.GetByIdAsync(sharingModel.StoredFolderId);

            await NotifyAboutSharing(folder.Title, acc.Id);

            resp.Item = AutoMapperConfig.Mapper.Map<SharedFolderModelApi<int>>(await CreateSharing(sharingModel, acc));

            return resp;
        } 

        private async Task<SharedFolderModelBussines<int>> CreateSharing(SharedFolderModelApi<int> sharingModel, AccountModelBussines<int> acc)
        {
            var mappedSharedFolder = AutoMapperConfig.Mapper.Map<SharedFolderModelBussines<int>>(sharingModel);

            mappedSharedFolder.AccountId = acc.Id;

            var res = await this._sharedFolderRepository.CreateAsync(mappedSharedFolder);
            return res;
        }

        internal async Task NotifyAboutSharing(string title, int recieverId)
        {
            var notification = new NotificationModelBussines<int>()
            {
                Descritpion = $"Folder {title} shared with you!",
                Title = $"Folder {title} shared with you!",
                NotificationState = SharedTypes.Enums.NotificationStateEnum.New,
                AccountId = recieverId
            };

            var res = await this._notificationRepository.CreateAsync(notification);

            await this._signalRCommunicationService.SendNotificationSignalR(
                AutoMapperConfig.Mapper.Map<NotificationModelApi<int>>(res),
                recieverId);
        }
    }
}
