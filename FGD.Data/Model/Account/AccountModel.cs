using System;

namespace FGD.Data
{
    public class AccountModel<TKey>
    {
        public TKey Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        #region Navigation props
        public AccountInfoModel<TKey> AccountInfo { get; set; }

        public AccountSubscriptionModel<TKey> AccountSubscription { get; set; }

        public NotificationModel<TKey> Notification { get; set; }

        public AccountCryptoModel<TKey> AccountCrypto { get; set; }
        #endregion
    }
}
