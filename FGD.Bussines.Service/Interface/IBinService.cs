using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface IBinService<TKey,TFileModel,TFolderModel>
    {
        Task<TFileModel> RestoreFileAsync(TKey Id);

        Task<TFileModel> DeleteFileForeverAsync(TKey Id, TKey UserId);

        Task<TFolderModel> RestoreFolderAsync(TKey Id);

        Task<TFolderModel> DeleteFolderForeverAsync(TKey Id, TKey UserId);

    }
}
