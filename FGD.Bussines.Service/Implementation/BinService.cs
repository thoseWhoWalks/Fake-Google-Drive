using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Configuration;
using FGD.Data.Service;
using System.IO;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public class BinService : IBinService<int, StoredFileModelApi<int>, StoredFolderModelApi<int>>
    {
        private IStoredFolderRepository<StoredFolderModelBussines<int>, int> _storedFolderRepository;

        private IStoredFileRepository<StoredFileModelBussines<int>, int> _storedFileRepository;

        private INotificationRepository<NotificationModelBussines<int>, int> _notificationRepository;

        private ISubscriptionCapacityService _subscriptionCapacityService;

        private ISignalRCommunicationService _signalRCommunicationService;

        private static int folderDeleteTransactionSize = 0;

        public BinService(IStoredFolderRepository<StoredFolderModelBussines<int>, int> storedFolderRepository,
                          IStoredFileRepository<StoredFileModelBussines<int>, int> storedFileRepository,
                          ISignalRCommunicationService signalRCommunicationService,
                          INotificationRepository<NotificationModelBussines<int>, int> notificationRepository,
                          ISubscriptionCapacityService subscriptionCapacityService 
                          )
        {
            this._storedFolderRepository = storedFolderRepository;
            this._storedFileRepository = storedFileRepository;
            this._signalRCommunicationService = signalRCommunicationService;
            this._notificationRepository = notificationRepository;
            this._subscriptionCapacityService = subscriptionCapacityService;
        }

        public async Task<StoredFileModelApi<int>> DeleteFileForeverAsync(int Id, int UserId)
        {
            var file = await this._storedFileRepository.GetByIdAsync(Id);

            await this._storedFileRepository.DeleteForeverById(Id);

            File.Delete(file.Path);
            
            await NotifyAboutDeletetionAsync(UserId, file.Title);

            await this._subscriptionCapacityService.DecreaseTakenSpaceAsync(file.SizeInKbs, UserId);

            return AutoMapperConfig.Mapper.Map<StoredFileModelApi<int>>(
                file
                );
        }
    
        public async Task<StoredFolderModelApi<int>> DeleteFolderForeverAsync(int Id, int UserId)
        {
            
            var folder = await this._storedFolderRepository.GetByIdAsync(Id);

            await DeleteFolderContentRecursivelyAsync(folder.Id);

            await this._storedFolderRepository.DeleteForeverByIdAsync(Id);

            await NotifyAboutDeletetionAsync(UserId, folder.Title);

            await this._subscriptionCapacityService.DecreaseTakenSpaceAsync(folderDeleteTransactionSize, UserId);

            folderDeleteTransactionSize = 0;

            return AutoMapperConfig.Mapper.Map<StoredFolderModelApi<int>>(
                folder
                );
        }

        private async Task DeleteFolderContentRecursivelyAsync(int folderId)
        {
            await DeleteFilesByFolderIdAsync(folderId);
              
            var subFolders = await this._storedFolderRepository.GetByParentFolderIdAsync(folderId);

            foreach (var folder in subFolders)
            {
                await this.DeleteFilesByFolderIdAsync(folder.Id);

                await this._storedFolderRepository.DeleteForeverByIdAsync(folder.Id);

                await DeleteFolderContentRecursivelyAsync(folder.Id);
            }

        }

        public async Task<StoredFileModelApi<int>> RestoreFileAsync(int Id)
        {
            var file = await this._storedFileRepository.GetByIdAsync(Id);

            file.IsDeleted = false;

            return AutoMapperConfig.Mapper.Map<StoredFileModelApi<int>>(
                await this._storedFileRepository.UpdateAsync(Id, file)
                );
        }

        public async Task<StoredFolderModelApi<int>> RestoreFolderAsync(int Id)
        {
            var folder = await this._storedFolderRepository.GetByIdAsync(Id);

            folder.IsDeleted = false;

            return AutoMapperConfig.Mapper.Map<StoredFolderModelApi<int>>(
                await this._storedFolderRepository.UpdateAsync(Id, folder)
                );
        }

        private async Task NotifyAboutDeletetionAsync(int userId, string title)
        {
            var notification = new NotificationModelBussines<int>()
            {
                Descritpion = $"{title} is deleted forever from your storage!",
                Title = $"{title} is deleted!",
                NotificationState = SharedTypes.Enums.NotificationStateEnum.New,
                AccountId = userId
            };

            var res = await this._notificationRepository.CreateAsync(notification);

            await this._signalRCommunicationService.SendNotificationSignalR(
                AutoMapperConfig.Mapper.Map<NotificationModelApi<int>>(res),
                userId);
        }

        private async Task DeleteFilesByFolderIdAsync(int folderId)
        {
            var filesParent = await this._storedFileRepository.GetByParentFolderIdAsync(folderId);

            foreach (var fileItem in filesParent)
            {
                var file = await this._storedFileRepository.GetByIdAsync(fileItem.Id);

                folderDeleteTransactionSize += file.SizeInKbs;

                File.Delete(file.Path);

                File.Delete(file.ThumbnailPath);

                await this._storedFileRepository.DeleteForeverById(file.Id);
            }
        }

    }
}
