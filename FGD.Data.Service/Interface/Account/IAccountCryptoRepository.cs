using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public interface IAccountCryptoRepository<TModel, TKey> : IRepository<TModel, TKey>
    {
        Task<TModel> GetByUserIdAsync(TKey id);

        Task<string> GetAESKeyByYserIdAsync(TKey id);

        Task<TModel> CreateAsync(TKey userId);
    }
}
