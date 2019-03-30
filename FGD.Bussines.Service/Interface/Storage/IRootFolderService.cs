using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface IRootFolderService<TModel,TKey>
    {
        Task<TModel> CreateRootFolderAsync(); 
    }
}
