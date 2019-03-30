using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public interface IAccountSubscriptionRepository<TModel, TKey> : IRepository<TModel, TKey>
    {
        Task<TModel> GetByUserEmailAsync(string email);

        Task<TModel> GetByUserIdAsync(int id);

    }
}
