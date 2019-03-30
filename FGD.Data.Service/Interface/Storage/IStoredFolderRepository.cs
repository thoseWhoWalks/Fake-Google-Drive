using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public interface IStoredFolderRepository<TModel, TKey> : IRepository<TModel, TKey>
    {
        Task<ICollection<TModel>> GetByRootIdAsync(TKey id);

        Task<ICollection<TModel>> GetByParentFolderIdAsync(TKey id);

        Task<ICollection<TModel>> GetAllDeletedAsync();

        Task DeleteForeverByIdAsync(TKey id);
    }
}
