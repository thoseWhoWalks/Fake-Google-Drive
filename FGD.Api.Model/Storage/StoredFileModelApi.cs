using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model 
{
    public class StoredFileModelApi<TKey>
    {
        public TKey Id { get; set; }

        public String Title { get; set; }

        public string Extention { get; set; }

        public String ThumbnailPath { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        public IFormFile File { get; set; }

        public int? StoredFolderId { get; set; }

        public int SizeInKbs { get; set; }
    }
}
