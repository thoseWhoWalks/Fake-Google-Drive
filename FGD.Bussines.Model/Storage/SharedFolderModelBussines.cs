using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model
{
    public class SharedFolderModelBussines<TKey>
    {
        public TKey Id { get; set; }

        public TKey StoredFolderId { get; set; }

        public TKey AccountId { get; set; } 
    }
}
