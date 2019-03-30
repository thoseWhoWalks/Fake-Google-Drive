using System;

namespace FGD.Data
{
    public class StoredFileModel<TKey>
    {

        public TKey Id { get; set; } 

        public string Path { get; set; }

        public string Title { get; set; }

        public string HashedTitle { get; set; }

        public DateTime DateCreated { get; set; }

        public string Extention { get; set; }

        public string ThumbnailPath { get; set; }

        public bool IsDeleted { get; set; }

        public int? StoredFolderId { get; set; }

        public int? RootFolderId { get; set; }

        public int SizeInKbs { get; set; }

        #region nav props

        public StoredFolderModel<TKey> StoredFolder { get; set; } 

        public RootFolderModel<TKey> RootFolder { get; set; }

        #endregion

    }
}
