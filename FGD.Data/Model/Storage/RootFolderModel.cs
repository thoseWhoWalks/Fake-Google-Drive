using System;
using System.Collections.Generic;

namespace FGD.Data
{
    public class RootFolderModel<T>
    {
        public T Id { get; set; }

        public String Path { get; set; }

        #region nav props

        public AccountSubscriptionModel<T> AccountSubscription { get; set; }

        public ICollection<StoredFolderModel<T>> StoredFolders { get; set; }

        public ICollection<StoredFileModel<T>> StoredFiles { get; set; }

        #endregion
    }
}
