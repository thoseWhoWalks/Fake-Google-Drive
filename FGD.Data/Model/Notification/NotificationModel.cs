using FGD.SharedTypes.Enums;
using System;
using System.Collections.Generic;

namespace FGD.Data
{
    public class NotificationModel<TKey>
    {
        public TKey Id { get; set; }

        public string Title { get; set; }

        public string Descritpion { get; set; }

        public bool IsDeleted { get; set; }

        public TKey AccountId { get; set; }

        public NotificationStateEnum NotificationState { get; set; }

        public ICollection<AccountModel<TKey>> Accounts { get; set; }

        public NotificationStateModel NotificationStateRelation { get; set; }

    }
}
