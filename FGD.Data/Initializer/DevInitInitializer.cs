using FGD.Encryption.Helpers;
using System.Linq;

namespace FGD.Data.Initializer
{
    public static class DevInitInitializer
    {
        public static bool Initialize(FakeGoogleDriveContext context)
        {

            if (context.Accounts.Any())
              return false;

            #region Insert Subscription

            var subtype = context.Subscriptions.Add(new SubscriptionModel<int>()
                {
                    Title = "Free",
                    Description = "Free Subscription with some info",
                    Price = 15,
                    TotalSpace = 15
                }
            );

            context.SaveChanges();
            #endregion

            #region insert account
            var saltedHash = Pbkdf2KeyDerivationHelper.GenerateSaltedHash("123");

            var account = context.Accounts.Add(new AccountModel<int>()
            {
                Email = "creatom@mail.com",
                Role = "Admin",
                Salt = saltedHash.Salt,
                PasswordHash = saltedHash.Hash
            });

            context.SaveChanges();

            context.AccountInfos.Add(new AccountInfoModel<int>()
            {
                AccountId = account.Entity.Id,
                FirstName = "Vladyslav",
                LastName = "Yakymenko",
                Age = 20
            });

            context.SaveChanges();
            #endregion

            #region insert root folder

            var rootfolder = context.RootFolders.Add(new RootFolderModel<int>()
            {
                Path = "/creatorroot"
            });

            context.SaveChanges();

            #endregion
                   
            #region insert account to subscrioption to root

            var accsub = context.AccountSubscriptions.Add(new AccountSubscriptionModel<int>()
            {
                    AccountId = account.Entity.Id,
                    DurationDays = 300,
                    RootFolderId = rootfolder.Entity.Id,
                    SubscriptionId = subtype.Entity.Id,
                    TakenSpace = 3,
                    IsActive = true
             });

            context.SaveChanges();

            #endregion
             
            #region insert NotificationState

            var notificationState = context.NotificationStates.Add(new NotificationStateModel()
            {
                Title = SharedTypes.Enums.NotificationStateEnum.New,
                
            });

            var notificationStateRead = context.NotificationStates.Add(new NotificationStateModel()
            {
                Title = SharedTypes.Enums.NotificationStateEnum.Read
            });


            context.SaveChanges();

            #endregion

            #region insert Notification

            var notification = context.Notifications.Add(new NotificationModel<int>()
            {
                Title = "Subscribtion Free Formed",
                Descritpion = "Information About Subscription",
                AccountId = account.Entity.Id,
                NotificationState = SharedTypes.Enums.NotificationStateEnum.New
            });

            context.SaveChanges();

            #endregion

            #region insert stored folder

            var storedfolder = context.StoredFolders.Add(new StoredFolderModel<int>
            {
                RootFolderId = rootfolder.Entity.Id,
                Size = 10,
                Title = "Parent Root Folder",
                HashTitle = "HashTitle",
                Path = "sdad"
            });
             
            context.SaveChanges();

            var storedfolderNested = context.StoredFolders.Add(new StoredFolderModel<int>
            {
                StoredFolderId = storedfolder.Entity.Id,
                Size = 30,
                Title = "Nested child folder",
                HashTitle = "HashTitle",
                Path = "sdad"
            });

            context.SaveChanges();

            #endregion

            #region insert stored file
            var storedFileRoot = context.StoredFiles.Add(new StoredFileModel<int>()
            {
                Title = "download.jpg",
                HashedTitle = "7894f22a-1cff-49ad-ba08-a4bd7c7d9b6b.jpg",
                Extention = ".jpg",
                ThumbnailPath = "C:\\Users\\minel\\source\\repos\\Diploma\\FakeGoogleDrive\\FakeGoogleDrive\\thumbnails//d5b825b7-78df-4224-bfae-55e5a612d817.jpg",
                IsDeleted = false,
                Path = "C:\\Users\\minel\\source\\repos\\Diploma\\FakeGoogleDrive\\FakeGoogleDrive\\storage\\49346D93C5ABCBACF7E3B4C05360C6C9F19BBF62F9BC16FBD08E914C9405AEDD\\7894f22a-1cff-49ad-ba08-a4bd7c7d9b6b.jpg",
                RootFolderId = rootfolder.Entity.Id,
                SizeInKbs = 6
            });

            var storedFile = context.StoredFiles.Add(new StoredFileModel<int>()
            {
                Title = "CaraDelavign.jpg",
                HashedTitle = "1515FC1587F550FA8F45A43122A6D3D59720A5865F3FACE9BF6E8CE10E0B3CB0.jpg",
                Extention = ".jpg",
                ThumbnailPath = "C:\\Users\\minel\\source\\repos\\Diploma\\FakeGoogleDrive\\FakeGoogleDrive\\thumbnails//fc483dee-a0b4-47f1-a2f8-3630e32c5b88.jpg",
                IsDeleted = false,
                Path = "C:\\Users\\minel\\source\\repos\\Diploma\\FakeGoogleDrive\\FakeGoogleDrive\\storage\\49346D93C5ABCBACF7E3B4C05360C6C9F19BBF62F9BC16FBD08E914C9405AEDD\\1515FC1587F550FA8F45A43122A6D3D59720A5865F3FACE9BF6E8CE10E0B3CB0.jpg",
                RootFolderId = storedfolder.Entity.Id,
                SizeInKbs = 203
            });

            context.SaveChanges();
            #endregion

            return true;
        }
    }
}
