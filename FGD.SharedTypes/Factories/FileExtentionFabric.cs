using FGD.SharedTypes.Enums;

namespace FGD.SharedTypes.Fabrics
{
    public static class FileExtention
    {
        public static FileExtentionEnum ResolveFileExtention(string contentType)
        {
            if (contentType.Contains("image"))
                return FileExtentionEnum.Image;

            return FileExtentionEnum.Unknown;
        }
    }
}
