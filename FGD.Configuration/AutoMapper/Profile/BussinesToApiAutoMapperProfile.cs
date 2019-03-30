using AutoMapper;
using FGD.Api.Model;
using FGD.Bussines.Model;

namespace FGD.Configuration.AutoMapper
{
    public class BussinesToApiAutoMapperProfile : Profile
    {
        public BussinesToApiAutoMapperProfile()
        {
            #region Account
            CreateMap<AccountModelApi<int>, AccountModelBussines<int>>();
            CreateMap<AccountModelBussines<int>, AccountModelApi<int>>();
            #endregion

            #region AccountSubscription
            CreateMap<AccountSubscriptionModelApi<int>, AccountSubscriptionModelBussines<int>>();
            CreateMap<AccountSubscriptionModelBussines<int>, AccountSubscriptionModelApi<int>>();
            #endregion

            #region Notification
            CreateMap<NotificationModelApi<int>, NotificationModelBussines<int>>();
            CreateMap<NotificationModelBussines<int>, NotificationModelApi<int>>();
            #endregion

            #region Stored Folder
            CreateMap<StoredFolderModelApi<int>, StoredFolderModelBussines<int>>();
            CreateMap<StoredFolderModelBussines<int>, StoredFolderModelApi<int>>();
            #endregion

            #region Stored File
            CreateMap<StoredFileModelApi<int>, StoredFileModelBussines<int>>();
            CreateMap<StoredFileModelBussines<int>, StoredFileModelApi<int>>();
            #endregion

            #region Shared File
            CreateMap<SharedFileModelApi<int>, SharedFileModelBussines<int>>();
            CreateMap<SharedFileModelBussines<int>, SharedFileModelApi<int>>();
            #endregion

            #region Shared Folder
            CreateMap<SharedFolderModelApi<int>, SharedFolderModelBussines<int>>();
            CreateMap<SharedFolderModelBussines<int>, SharedFolderModelApi<int>>();
            #endregion
        }

    }
}
