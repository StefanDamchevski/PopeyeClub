using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;
using LazZiya.ImageResize;

namespace PopeyeClub.Helpers
{
    public static class FileToByteArray
    {
        internal static byte[] ToByteArray(this IFormFile profilePicture)
        {
            if(profilePicture == null)
            {
                return null;
            }
            MemoryStream stream = new MemoryStream();
            profilePicture.CopyTo(stream);
            Image scaledImage = ImageResize.ScaleByWidth(Image.FromStream(stream), 400);
            return (byte[])(new ImageConverter()).ConvertTo(scaledImage, typeof(byte[]));
        }
    }
}