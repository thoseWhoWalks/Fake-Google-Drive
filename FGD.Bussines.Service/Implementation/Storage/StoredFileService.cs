using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Bussines.Service.Helper;
using FGD.Configuration;
using FGD.Data.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace FGD.Bussines.Service
{
    public class StoredFileService : IStoredFileService<StoredFileModelApi<int>, int>
    {
        private IStoredFileRepository<StoredFileModelBussines<int>, int> _storedFileRepository;

        private IStoredFolderRepository<StoredFolderModelBussines<int>, int> _storedFolderRepository;

        private IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int> _accountSubscriptionRepository;

        private IRootFolderRepository<RootFolderModelBussines<int>, int> _rootFolderRepository;

        private ISubscriptionCapacityService _subscriptionCapacityService;

        private HostingEnvironmentHelper _hostingEnvironmentHelper;

        private ThumbnailHelper _thumbnailHelper;

        public StoredFileService(IStoredFileRepository<StoredFileModelBussines<int>, int> storedFileRepository,
            IStoredFolderRepository<StoredFolderModelBussines<int>, int> storedFolderRepository,
            IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int> accountSubscriptionRepository,
            IRootFolderRepository<RootFolderModelBussines<int>, int> rootFolderRepository,
            ISubscriptionCapacityService subscriptionCapacityService,
            HostingEnvironmentHelper hostingEnvironmentHelper,
            ThumbnailHelper thumbnailHelper
            )
        {
            this._storedFileRepository = storedFileRepository;
            this._storedFolderRepository = storedFolderRepository;
            this._accountSubscriptionRepository = accountSubscriptionRepository;
            this._rootFolderRepository = rootFolderRepository;
            this._hostingEnvironmentHelper = hostingEnvironmentHelper;
            this._thumbnailHelper = thumbnailHelper;
            this._subscriptionCapacityService = subscriptionCapacityService;
        }

        public async Task<StoredFileModelApi<int>> CreateAsync(StoredFileModelApi<int> model, int userId)
        {

            var response = await ProceseFiles(model, userId);

            await this._subscriptionCapacityService.IncreaseTakenSpace(response.SizeInKbs, userId);

            return response;
        }

        private async Task<StoredFileModelApi<int>> ProceseFiles(StoredFileModelApi<int> model, int userId)
        {
            var response = new StoredFileModelApi<int>();

            if (!await this._subscriptionCapacityService.IsFreeForDataPack(model.File.Length, userId))
                return response;

            var mappedModel = await PrepareModel(model, userId, model.File);

            var fs = new FileStream(
                    mappedModel.Path,
                   FileMode.CreateNew
                );

            await model.File.CopyToAsync(fs);

            fs.Close();

            mappedModel.ThumbnailPath = this._thumbnailHelper
                .CreateThumbnail(model.File.ContentType, model.File.OpenReadStream());

            response = AutoMapperConfig.Mapper.Map<StoredFileModelApi<int>>(
                    await this._storedFileRepository.CreateAsync(mappedModel)
                );

            return response;
        }

        private async Task<StoredFileModelBussines<int>> PrepareModel(StoredFileModelApi<int> model, int userId, IFormFile file)
        {
            var mappedModel = AutoMapperConfig.Mapper.Map<StoredFileModelBussines<int>>(model);

            if (mappedModel.StoredFolderId == null)
                mappedModel.RootFolderId = await this.GetRootFolderIdByUserIdAsync(userId); ;

            mappedModel.Title = Path.GetFileNameWithoutExtension(file.FileName);

            mappedModel.HashedTitle = Crypto.Hash("".GerRandomString()) +
                Path.GetExtension(file.FileName);

            mappedModel.SizeInKbs = file.Length.BytesToKilobytes();

            mappedModel.Extention = Path.GetExtension(file.FileName);

            var rootPath = await this.GetRootFolderPathByUserIdAsync(userId);

            mappedModel.Path = rootPath + "/" + mappedModel.HashedTitle;

            return mappedModel;
        }

        public Task<StoredFileModelApi<int>> CreateAsync(StoredFileModelApi<int> model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            return await this._storedFileRepository.DeleteByIdAsync(Id);
        }

        public async Task<ICollection<StoredFileModelApi<int>>> GetAllAsync()
        {
            return AutoMapperConfig.Mapper.Map<ICollection<StoredFileModelApi<int>>>(
                   await this._storedFileRepository.GetAllAsync()
                );
        }

        public async Task<ICollection<StoredFileModelApi<int>>> GetByStoredFolderIdAsync(int parentId)
        {
            return AutoMapperConfig.Mapper.Map<ICollection<StoredFileModelApi<int>>>(
                     await this._storedFileRepository.GetByParentFolderIdAsync(parentId)
               );
        }

        public async Task<ICollection<StoredFileModelApi<int>>> GetRootByUserIdAsync(int userId)
        {
            return AutoMapperConfig.Mapper.Map<ICollection<StoredFileModelApi<int>>>(
                    await this._storedFileRepository.GetByRootIdAsync(
                           await this.GetRootFolderIdByUserIdAsync(userId)
                         )
               );
        }

        public async Task<StoredFileModelApi<int>> UpdateAsync(int Id, StoredFileModelApi<int> model)
        {
            var mapped = AutoMapperConfig.Mapper.Map<StoredFileModelBussines<int>>(model);

            return AutoMapperConfig.Mapper.Map<StoredFileModelApi<int>>(
                    await this._storedFileRepository.UpdateAsync(Id, mapped)
                );
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

        public async Task<DownloadApiModel> DownloadById(int Id)
        {

            var file = await this._storedFileRepository.GetByIdAsync(Id);

            var stream = new FileStream(file.Path, FileMode.Open);

            var download = new DownloadApiModel()
            {
                Title = file.Title,
                FileStream = stream,
                ContentType = new ContentType("application/x-msdownload")
            };

            return download;
        }

        public async Task<ICollection<StoredFileModelApi<int>>> GetDeletedByUserId(int Id)
        {
            var rootFolderId = await this.GetRootFolderIdByUserIdAsync(Id);

            var res = new List<StoredFileModelApi<int>>();

            var rootFiles = await this._storedFileRepository.GetByRootIdAsync(rootFolderId);

            foreach (var file in rootFiles)
                if (file.IsDeleted)
                    res.Add(AutoMapperConfig.Mapper.Map<StoredFileModelApi<int>>(file));

            var rootFolders = await this._storedFolderRepository.GetByRootIdAsync(rootFolderId);

            foreach (var folder in rootFolders)
                if (!folder.IsDeleted)
                    res = await this.GetDeletedRecursivelyAsync(folder.Id, res);

            return res;
        }

        private async Task<List<StoredFileModelApi<int>>> GetDeletedRecursivelyAsync(int Id, List<StoredFileModelApi<int>> res)
        {

            var files = await this._storedFileRepository.GetByParentFolderIdAsync(Id);

            foreach (var file in files)
                if (file.IsDeleted)
                    res.Add(AutoMapperConfig.Mapper.Map<StoredFileModelApi<int>>(file));

            var folders = await this._storedFolderRepository.GetByParentFolderIdAsync(Id);

            foreach (var folder in folders)
                if (!folder.IsDeleted)
                    return await this.GetDeletedRecursivelyAsync(folder.Id, res);

            return res;
        }
    }
}
