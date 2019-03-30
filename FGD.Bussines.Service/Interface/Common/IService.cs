using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface IService<TModel,TKey>
    {

        Task<TModel> CreateAsync(TModel model);

        Task<bool> DeleteAsync(int Id);

        Task<TModel> UpdateAsync(TKey Id,TModel model);

        Task<ICollection<TModel>> GetAllAsync();

    }
}
