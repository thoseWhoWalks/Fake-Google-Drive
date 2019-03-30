namespace FGD.Bussines.Service.Util
{
    public static class RedisCachingKeysUtil
    {
        public const string GET_ALL_ACCOUNTS_KEY = "AccountService:GetAll:Account";

        public const string GET_ACCOUNT_BY_EMAIL_KEY = "AccountService:GetByEmail:";

        public const string GET_NOTIFICATIONS_BY_USER_ID_KEY = "NotificationService:GetAllByUserId:";

        public const string GET_SUBSCRIPTION_BY_ID_KEY = "SubscriptionService:GetById:";

        public const string GET_SHARED_FILES_BY_USER_ID = "SharedFilesService:GetFilesByRootId:";

        public const string GET_ALL_SHARINGS_BY_FILE_ID = "SharedFilesService:GetFileById:";

        public const string GET_SHARED_FOLDERS_BY_USER_ID = "SharedFolderService:GetFilesByRootId:";

        public const string GET_ALL_SHARINGS_BY_FOLDER_ID = "SharedFolderService:GetFileById:";

    }
}
