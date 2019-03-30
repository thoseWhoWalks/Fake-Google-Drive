using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Web.Helpers;

namespace FGD.Bussines.Service.Helper
{
    public class HostingEnvironmentHelper
    {
        private IHostingEnvironment _hostingEnvironment;

        public HostingEnvironmentHelper(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public string CreateRootFolder()
        {
            string rootProjectFolder = this._hostingEnvironment.ContentRootPath + "\\storage\\" + Crypto.Hash("".GerRandomString());

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
