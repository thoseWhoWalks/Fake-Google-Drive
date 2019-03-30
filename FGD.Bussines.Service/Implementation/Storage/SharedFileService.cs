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
    public class SharedFileService : ISharedFileService<int, SharedFileModelApi<int>, StoredFileModelApi<int>>
    {
        private IStoredFileRepository<StoredFileModelBussines<int>, int> _storedFileRepository;

        private ISharedFileRepository<SharedFileModelBussines<int>, int> _sharedFileRepository;

        private IAccountRepository<AccountModelBussines<int>, int> _accountRepository;

        private INotificationRepository<NotificationModelBussines<int>, int> _notificationRepository;

        private ISignalRCommunicationService _signalRCommunicationService;

        private IRedisCachingService _redisCachingService;

        public SharedFileService(
                IStoredFileRepository<StoredFileModelBussines<int>, int> storedFileRepository,
                ISharedFileRepository<SharedFileModelBussines<int>, int> sharedFileRepository,
                IAccountRepository<AccountModelBussines<int>, int> accountRepository,
                INotificationRepository<NotificationModelBussines<int>, int> notificationRepository,
                ISignalRCommunicationService signalRCommunicationService,
                IRedisCachingService redisCachingService
            )
        {
            this._storedFileRepository = storedFileRepository;
            this._sharedFileRepository = sharedFileRepository;
            this._accountRepository = accountRepository;
            this._redisCachingService = redisCachingService;
            this._notificationRepository = notificationRepository;
            this._signalRCommunicationService = signalRCommunicationService;
        }

        public async Task<ICollection<StoredFileModelApi<int>>> GetAllByUserId(int UserId)
        {
            ICollection<SharedFileModelBussines<int>> sharedFiles = await getFromCasheOrDb(UserId);

            return await ConvertToStoredFiles(sharedFiles);
        }

        private async Task<List<StoredFileModelApi<int>>> ConvertToStoredFiles(ICollection<SharedFileModelBussines<int>> sharedFiles)
        {
            var storedFiles = new List<StoredFileModelApi<int>>();

            foreach (var file in sharedFiles)
                storedFiles.Add(AutoMapperConfig.Mapper.Map<StoredFileModelApi<int>>(
                       await this._storedFileRepository.GetByIdAsync(file.StoredFileId)
                    ));
            return storedFiles;
        }

        private async Task<ICollection<SharedFileModelBussines<int>>> getFromCasheOrDb(int UserId)
        {
            var sharedFiles = await this._redisCachingService.GetItemAsync<ICollection<SharedFileModelBussines<int>>>(
                RedisCachingKeysUtil.GET_SHARED_FILES_BY_USER_ID + UserId
                );

            if (sharedFiles == null)
            {
                sharedFiles = await this._sharedFileRepository.GetAllByUserIdAsync(UserId);

                await this._redisCachingService.PutItemAsync<ICollection<SharedFileModelBussines<int>>>(
                        RedisCachingKeysUtil.GET_SHARED_FILES_BY_USER_ID + UserId,
                        sharedFiles,
                        new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(10)
                        }
                        );
            }

            return sharedFiles;
        }

        public async Task<ResponseModel<SharedFileModelApi<int>>> ShareFile(SharedFileModelApi<int> sharingModel)
        {
            var acc = await this._accountRepository.GetByEmailAsync(sharingModel.AccountEmail);

            var resp = new ResponseModel<SharedFileModelApi<int>>();

            if (acc == null)
            {
                resp.AddError(new Error($"This user not exists"));
                return resp;
            }

            await this._redisCachingService.RemoveItemAsync(RedisCachingKeysUtil.GET_SHARED_FILES_BY_USER_ID + acc.Id);

            var file = await this._storedFileRepository.GetByIdAsync(sharingModel.StoredFileId);

            await NotifyAboutSharing(file.Title, acc.Id);

            resp.Item = AutoMapperConfig.Mapper.Map<SharedFileModelApi<int>>(await CreateSharing(sharingModel, acc));

            return resp;
        }

        private async Task<SharedFileModelBussines<int>> CreateSharing(SharedFileModelApi<int> sharingModel, AccountModelBussines<int> acc)
        {
            var mappedSharedFile = AutoMapperConfig.Mapper.Map<SharedFileModelBussines<int>>(sharingModel);

            mappedSharedFile.AccountId = acc.Id;

            var res = await this._sharedFileRepository.CreateAsync(mappedSharedFile);
            return res;
        }

        internal async Task NotifyAboutSharing(string itemTitle, int recieverId)
        {
            var notification = new NotificationModelBussines<int>()
            {
                Descritpion = $"File {itemTitle} shared with you!",
                Title = $"File {itemTitle} shared with you!",
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
