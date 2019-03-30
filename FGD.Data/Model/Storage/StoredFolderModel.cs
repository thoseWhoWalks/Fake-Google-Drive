using System;
using System.Collections.Generic;

namespace FGD.Data
{
    public class StoredFolderModel<TKey>
    {
        public TKey Id { get; set; }

        public string Path { get; set; }

        public string Title { get; set; }

        public string HashTitle { get; set; }

        public float Size { get; set; }

        public DateTime DateCreated { get; set; }

        public int? StoredFolderId { get; set; }
        
        public bool IsDeleted { get; set; }

        public int? RootFolderId { get; set; }

        #region nav props

        public RootFolderModel<TKey> RootFolder { get; set; } 

        public StoredFolderModel<TKey> StoredFolder { get; set; }

        public ICollection<StoredFileModel<TKey>> StoredFiles { get; set; }
        #endregion

    }
}
