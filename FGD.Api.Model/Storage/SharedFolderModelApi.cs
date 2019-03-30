using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model
{
    public class SharedFolderModelApi<TKey>
    {
        public TKey Id { get; set; }

        public TKey StoredFolderId { get; set; }

        public string AccountEmail { get; set; }
         
    }
}
