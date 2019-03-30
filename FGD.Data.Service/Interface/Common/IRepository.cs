using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public interface IRepository<TModel, TKey>
    { 
        Task<ICollection<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(TKey id);

        Task<TModel> CreateAsync(TModel model);

        Task<TModel> UpdateAsync(TKey id, TModel model);

        Task<bool> DeleteByIdAsync(TKey id);
    }
}
