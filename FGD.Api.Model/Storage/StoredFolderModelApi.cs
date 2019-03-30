using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model 
{
    public class StoredFolderModelApi<TKey>
    {
        public TKey Id { get; set; } 

        public String Title { get; set; }

        public float Size { get; set; }

        public DateTime DateCreated { get; set; }

        public int? StoredFolderId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
