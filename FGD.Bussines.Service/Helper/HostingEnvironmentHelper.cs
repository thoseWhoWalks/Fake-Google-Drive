using FGD.Encryption.Helpers;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace FGD.Bussines.Service.Helper
{
    public class HostingEnvironmentHelper
    {
        private IHostEnvironment _hostingEnvironment;

        public HostingEnvironmentHelper(IHostEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public string CreateRootFolder()
        {
            string rootProjectFolder = this._hostingEnvironment.ContentRootPath + "\\storage\\" + SHA256HashHelper .Hash("".GerRandomString());

            if (!Directory.Exists(rootProjectFolder))
                Directory.CreateDirectory(rootProjectFolder);

            return rootProjectFolder;
        }

        public string CreateThumbnailFolder()
        {
            string rootThumbnailFolderPath = this._hostingEnvironment.ContentRootPath + "\\thumbnails";
            
            if (!Directory.Exists(rootThumbnailFolderPath))
                Directory.CreateDirectory(rootThumbnailFolderPath);

            return rootThumbnailFolderPath;
        }

    }
}
