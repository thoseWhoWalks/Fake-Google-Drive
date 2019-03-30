using AutoMapper;
using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Data;

namespace FGD.Configuration.AutoMapper
{
    public class DataToBussinesAutoMapperProfile : Profile
    { 
        public DataToBussinesAutoMapperProfile() {

            #region Account
            CreateMap<AccountModel<int>, AccountModelBussines<int>>();
            CreateMap<AccountInfoModel<int>, AccountModelBussines<int>>()
                .ForMember(ab=>ab.Id,a=>a.Ignore());

            CreateMap<AccountModelBussines<int>, AccountModel<int>>();
            CreateMap<AccountModelBussines<int>, AccountInfoModel<int>>()
                .ForMember(ab => ab.Id, a => a.Ignore());
            #endregion

            #region AccountCrypto

            CreateMap<AccountCryptoModel<int>, AccountCryptoModelBussines<int>>();
            CreateMap<AccountCryptoModelBussines<int>, AccountCryptoModel<int>>();

            #endregion

            #region Subscription

            CreateMap<SubscriptionModel<int>, SubscriptionModelApi<int>>();
            CreateMap<SubscriptionModelApi<int>, SubscriptionModel<int>>();

            #endregion

            #region  AccountSubscription
            CreateMap<AccountSubscriptionModel<int>, AccountSubscriptionModelBussines<int>>();
            CreateMap<AccountSubscriptionModelBussines<int>, AccountSubscriptionModel<int>>();
            #endregion

            #region RootFolder
            CreateMap<RootFolderModel<int>, RootFolderModelBussines<int>>();
            CreateMap<RootFolderModelBussines<int>, RootFolderModel<int>>();
            #endregion

            #region Notification
            CreateMap<NotificationModel<int>, NotificationModelBussines<int>>();
            CreateMap<NotificationModelBussines<int>, NotificationModel<int>>();
            #endregion

            #region Stored Folder
            CreateMap<StoredFolderModel<int>, StoredFolderModelBussines<int>>();
            CreateMap<StoredFolderModelBussines<int>, StoredFolderModel<int>>();
            #endregion

            #region Stored File 
            CreateMap<StoredFileModelBussines<int>, StoredFileModel<int>>();
            CreateMap<StoredFileModel<int>, StoredFileModelBussines<int>>();
            #endregion

            #region Shared File 
            CreateMap<SharedFileModelBussines<int>, SharedFileModel<int>>();
            CreateMap<SharedFileModel<int>, SharedFileModelBussines<int>>();
            #endregion

            #region Shared Folder 
            CreateMap<SharedFolderModelBussines<int>, SharedFolderModel<int>>();
            CreateMap<SharedFolderModel<int>, SharedFolderModelBussines<int>>();
            #endregion
        }
    }
}
