using FGD.Api.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service 
{
    public interface IStoredFileService<TModel,TKey> : IService<TModel,TKey>
    {
        Task<ICollection<TModel>> GetRootByUserIdAsync(TKey userId);

        Task<ICollection<TModel>> GetByStoredFolderIdAsync(TKey parentId);

        Task<TModel> CreateAsync(TModel model, TKey UserId);

        Task<DownloadApiModel> DownloadById(TKey Id);

        Task<ICollection<TModel>> GetDeletedByUserId(TKey Id); 
    }
}
