using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Configuration;
using FGD.Data.Service;
using FGD.Encryption.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service 
{
    public class StoredFolderService : IStoredFolderService<StoredFolderModelApi<int>,int>
    {
        private IStoredFolderRepository<StoredFolderModelBussines<int>, int> _storedFolderRepository;

        private IRootFolderRepository<RootFolderModelBussines<int>, int> _rootFolderRepository;

        private IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int> _accountSubscriptionRepository;

        public StoredFolderService(IStoredFolderRepository<StoredFolderModelBussines<int>, int> storedFolderRepository,
            IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int> accountSubscriptionRepository,
            IRootFolderRepository<RootFolderModelBussines<int>, int> rootFolderRepository)
        {
            this._storedFolderRepository = storedFolderRepository;
            this._accountSubscriptionRepository = accountSubscriptionRepository;
            this._rootFolderRepository = rootFolderRepository;
        }

        public async Task<StoredFolderModelApi<int>> CreateAsync(StoredFolderModelApi<int> model,int UserId)
        {

            var mappedModel = AutoMapperConfig.Mapper.Map<StoredFolderModelBussines<int>>(model);

            if (mappedModel.StoredFolderId == null)
                mappedModel.RootFolderId = await this.GetRootFolderIdByUserIdAsync(UserId);
            
            mappedModel.Path = await this.GetRootFolderPathByUserIdAsync(UserId);

            mappedModel.HashTitle = SHA256HashHelper.Hash(mappedModel.Title);

            var res = await this._storedFolderRepository.CreateAsync(
                  mappedModel
                );

            return AutoMapperConfig.Mapper.Map<StoredFolderModelApi<int>>(res);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var res = await this._storedFolderRepository.DeleteByIdAsync(Id);

            return res; 
        }

        public async Task<ICollection<StoredFolderModelApi<int>>> GetAllAsync()
        {
            var res = await this.GetAllAsync();

            return AutoMapperConfig.Mapper.Map<ICollection<StoredFolderModelApi<int>>>(res);
        }

        public async Task<ICollection<StoredFolderModelApi<int>>> GetByParentIdAsync(int parentId)
        {
            var folders = await this._storedFolderRepository.GetByParentFolderIdAsync(parentId);

            return AutoMapperConfig.Mapper.Map<ICollection<StoredFolderModelApi<int>>>(folders);
        }

        public async Task<ICollection<StoredFolderModelApi<int>>> GetRootByUserIdAsync(int userId)
        {
            var folders = await this._storedFolderRepository.GetByRootIdAsync(
                    await this.GetRootFolderIdByUserIdAsync(userId)
                );

            return AutoMapperConfig.Mapper.Map<ICollection<StoredFolderModelApi<int>>>(folders);
        }

        public async Task<StoredFolderModelApi<int>> UpdateAsync(int Id,StoredFolderModelApi<int> model)
        {
            var res = await this._storedFolderRepository.UpdateAsync(Id,
                    AutoMapperConfig.Mapper.Map<StoredFolderModelBussines<int>>(model)
                );

            return AutoMapperConfig.Mapper.Map<StoredFolderModelApi<int>>(res);
        }
 
        private async Task<int> GetRootFolderIdByUserIdAsync(int userId)
        {
            var accountSubscription = await this._accountSubscriptionRepository.GetByUserIdAsync(userId);

            return accountSubscription.RootFolderId;
        }

        private async Task<string> GetRootFolderPathByUserIdAsync(int userId)
        {
            var accountSubscription = await this._accountSubscriptionRepository.GetByUserIdAsync(userId);

            var rootFolder = await this._rootFolderRepository.GetByIdAsync(
                accountSubscription.RootFolderId
                );

            return rootFolder.Path;
        }

        public async Task<ICollection<StoredFolderModelApi<int>>> GetDeletedByUserId(int Id)
        {
            var rootFolderId = await this.GetRootFolderIdByUserIdAsync(Id);

            var res = new List<StoredFolderModelApi<int>>();

            var rootFolders = await this._storedFolderRepository.GetByRootIdAsync(rootFolderId);

            foreach (var folder in rootFolders)
                if (folder.IsDeleted)
                    res.Add(AutoMapperConfig.Mapper.Map<StoredFolderModelApi<int>>(folder));

            foreach (var folder in rootFolders)
                if (!folder.IsDeleted)
                    res = await this.GetDeletedRecursivelyAsync(folder.Id, res);

            return res;
        }

        private async Task<List<StoredFolderModelApi<int>>> GetDeletedRecursivelyAsync(int Id, List<StoredFolderModelApi<int>> res)
        {

            var folders = await this._storedFolderRepository.GetByParentFolderIdAsync(Id);

            foreach (var folder in folders)
                if (folder.IsDeleted)
                    res.Add(AutoMapperConfig.Mapper.Map<StoredFolderModelApi<int>>(folder));


            foreach (var folder in folders)
                if (!folder.IsDeleted)
                    await this.GetDeletedRecursivelyAsync(folder.Id, res);

            return res;
        }

        public Task<StoredFolderModelApi<int>> CreateAsync(StoredFolderModelApi<int> model)
        {
            throw new NotImplementedException();
        }
    }
}
