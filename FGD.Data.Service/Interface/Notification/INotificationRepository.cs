using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public interface INotificationRepository<TModel, TKey> : IRepository<TModel, TKey>
    {
        Task<ICollection<TModel>> GetAllByUserId(TKey id);
    }
}
