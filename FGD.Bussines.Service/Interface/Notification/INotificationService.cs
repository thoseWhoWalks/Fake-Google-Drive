using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service 
{
    public interface INotificationService<TModel,TKey>
    {
        Task<ICollection<TModel>> GetAllByUserId(TKey Id);

        Task<TModel> UpdateState(TKey Id);
        
    }
}
