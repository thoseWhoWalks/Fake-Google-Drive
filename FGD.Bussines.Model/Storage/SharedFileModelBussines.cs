using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model
{ 
    public class SharedFileModelBussines<TKey>
    {
        public TKey Id { get; set; }

        public TKey StoredFileId { get; set; }

        public TKey AccountId { get; set; }

    }
}
