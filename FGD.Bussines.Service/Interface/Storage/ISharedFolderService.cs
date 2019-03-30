using FGD.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface ISharedFolderService<TKey, TModel, TResModel>
    {

        Task<ResponseModel<TModel>> ShareFolder(TModel sharingModel);

        Task<ICollection<TResModel>> GetAllByUserId(TKey UserId);

    }
}
