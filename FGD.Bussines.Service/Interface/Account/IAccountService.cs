using FGD.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface IAccountService<TModel, TKey>
    {
        Task<ResponseModel<TModel>> GetUserByEmailAsync(string email);

        Task<ResponseModel<TModel>> RegisterUserAsync(TModel model);

        Task<ResponseModel<ICollection<AccountModelApi<int>>>> GetAllAsync();

    }
}
