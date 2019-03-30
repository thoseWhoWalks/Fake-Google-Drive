using FGD.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface ISharedFileService<TKey,TModel,TResModel>
    {

        Task<ResponseModel<TModel>> ShareFile(TModel sharingModel);

        Task<ICollection<TResModel>> GetAllByUserId(TKey UserId);
        
    }
}
