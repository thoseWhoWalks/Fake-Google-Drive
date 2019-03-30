using FGD.Api.Model;
using System;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public interface IAccountRepository<TModel, TKey> : IRepository<TModel, TKey>
    {
        Task<bool> VerifyUserAsync(LoginModelApi loginModel);

        Task<TModel> GetByEmailAsync(String email);
    }
}
