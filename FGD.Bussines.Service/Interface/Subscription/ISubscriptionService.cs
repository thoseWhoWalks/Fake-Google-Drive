using FGD.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface ISubscriptionService<TModel, TKey>
    {
        Task<TModel> GetByIdAsync(TKey Id);

    }
}
