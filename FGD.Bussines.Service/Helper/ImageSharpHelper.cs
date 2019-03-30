using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace FGD.Bussines.Service.Helper
{
    public class ImageSharpHelper
    {
        private Image<Rgba32> image = null; 
         
        public ImageSharpHelper()
        {
            
        }

        public Image<Rgba32> ResizeThumbnail(int h, int w, Stream imageStream)
        {

            this.image = Image.Load(imageStream);

            this.image.Mutate(x => x
                .Resize(w, h));

              
            return this.image;          
        }

    }
}
