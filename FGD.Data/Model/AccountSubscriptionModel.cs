using System;
using System.Collections.Generic;

namespace FGD.Data
{
    public class AccountSubscriptionModel<TKey>
    {
        public TKey Id { get; set; }

        public TKey AccountId { get; set; } 

        public TKey SubscriptionId { get; set; }

        public TKey RootFolderId { get; set; }

        public bool IsActive { get; set; }

        public int TakenSpace { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationDays { get; set; }

        #region navigation properties

        public ICollection<AccountModel<TKey>> Accounts { get; set; }

        public SubscriptionModel<TKey> Subscription { get; set; }

        public RootFolderModel<TKey> RootFolder { get; set; }

        #endregion

    }
}
