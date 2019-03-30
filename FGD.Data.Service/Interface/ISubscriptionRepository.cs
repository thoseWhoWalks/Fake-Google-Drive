using FGD.Api.Model;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public interface ISubscriptionRepository<TModel, TKey> : IRepository<TModel, TKey>
    {
        Task<SubscriptionModelApi<int>> GetByTitleAsync(string title);
    }
}
