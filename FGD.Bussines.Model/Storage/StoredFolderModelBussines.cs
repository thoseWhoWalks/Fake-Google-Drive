using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model 
{
    public class StoredFolderModelBussines<TKey>
    {
        public TKey Id { get; set; }

        public String Path { get; set; }

        public String Title { get; set; }

        public String HashTitle { get; set; }

        public float Size { get; set; }

        public DateTime DateCreated { get; set; }

        //ToDo:Resolve Nullable generic
        public int? StoredFolderId { get; set; }

        public bool IsDeleted { get; set; }

        public int? RootFolderId { get; set; }
         
    }
}
