using System.IO;
using System.Net.Mime;

namespace FGD.Api.Model
{
    public class DownloadApiModel
    { 
        public ContentType ContentType { get; set; }

        public FileStream FileStream { get; set; }

        public string Title { get; set; }
    }
}
