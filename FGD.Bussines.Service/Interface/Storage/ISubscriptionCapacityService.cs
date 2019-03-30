using FGD.Api.Model;
using FGD.Bussines.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface ISubscriptionCapacityService
    {

        Task<AccountSubscriptionModelBussines<int>> IncreaseTakenSpace(int kbs, int userId);

        Task<AccountSubscriptionModelBussines<int>> DecreaseTakenSpaceAsync(int kbs, int userId);

        Task<bool> IsFreeForDataPack(long bytes,int userId);
    }
}
