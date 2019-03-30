using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model
{
    public class SharedFileModelApi<TKey>
    {
        public TKey Id { get; set; }

        public TKey StoredFileId { get; set; }

        public string AccountEmail { get; set; }
    }
}
