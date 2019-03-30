using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service 
{
    public interface IStoredFolderService<TModel, TKey> : IService<TModel, TKey>
    {
        Task<ICollection<TModel>> GetRootByUserIdAsync(TKey userId);

        Task<ICollection<TModel>> GetByParentIdAsync(TKey parentId);

        Task<TModel> CreateAsync(TModel model, TKey UserId);

        Task<ICollection<TModel>> GetDeletedByUserId(TKey Id);
    }
}
