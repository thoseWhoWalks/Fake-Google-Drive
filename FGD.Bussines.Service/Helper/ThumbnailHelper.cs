using FGD.Encryption.Helpers;
using FGD.SharedTypes.Enums;
using FGD.SharedTypes.Fabrics;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace FGD.Bussines.Service.Helper
{
    public class ThumbnailHelper
    {
        public static int H = 100;
        public static int W = 150;

        Dictionary<FileExtentionEnum, Func<Stream,string>> thumbnailGenerators = new Dictionary<FileExtentionEnum, Func<Stream, string>>();

        ImageSharpHelper _imageSharpHelper;
       
        private HostingEnvironmentHelper _hostingEnvironmentHelper;

        public ThumbnailHelper(ImageSharpHelper imageSharpHelper,
               HostingEnvironmentHelper hostingEnvironmentHelper)
        {
            this._hostingEnvironmentHelper = hostingEnvironmentHelper;

            this._imageSharpHelper = imageSharpHelper;
            
            FillThumbnailGenerators();
        }

        private void FillThumbnailGenerators()
        {
            thumbnailGenerators.Add(
                    FileExtentionEnum.Unknown,
                    (stream) => null
                );

            thumbnailGenerators.Add(
                    FileExtentionEnum.Image,
                    (stream) =>
                        {
                            var image = this._imageSharpHelper.ResizeThumbnail(
                                ThumbnailHelper.H, 
                                ThumbnailHelper.W,
                                stream
                                );

                            var fullPath = GetFullPathToThumbnail();

                            image.Save(fullPath);

                            return fullPath;
                        }
                );
        }

        public string CreateThumbnail(string contentType, Stream fileStream)
        {
            var type = FileExtention.ResolveFileExtention(contentType);

            var res = ConvertToBase64(
                this.thumbnailGenerators.GetValueOrDefault(type).Invoke(fileStream)
                );

            fileStream.Close();
            
            return res;

        }

        private string ConvertToBase64(string thumbnailPath)
        {

            if (thumbnailPath == null || !File.Exists(thumbnailPath))
                return null;

            var bytes = File.ReadAllBytes(thumbnailPath);

            return "data:image/png;base64," + Convert.ToBase64String(bytes);

        }

        private string GetFullPathToThumbnail()
        {
            return this._hostingEnvironmentHelper.CreateThumbnailFolder() + "//" +
                SHA256HashHelper.Hash("".GerRandomString()) + ".jpg";
        }
         
    }
}
