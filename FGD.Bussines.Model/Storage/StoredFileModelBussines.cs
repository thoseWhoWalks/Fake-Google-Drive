using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model
{
    public class StoredFileModelBussines<TKey>
    {
        public TKey Id { get; set; }

        public String Path { get; set; }

        public String Title { get; set; }

        public String HashedTitle { get; set; }

        public string Extention { get; set; }

        public String ThumbnailPath { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public int? StoredFolderId { get; set; }

        public int? RootFolderId { get; set; }

        public int SizeInKbs { get; set; }
         
    }
}
