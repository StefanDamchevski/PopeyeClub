using System.IO;

namespace PopeyeClub.Services.Helpers
{
    public static class StringToByteArray
    {
        internal static byte[] ToByteArray(this string profilePicture)
        {
            FileStream fileStream = new FileStream(profilePicture, FileMode.Open);
            byte[] picture = new byte[fileStream.Length];
            fileStream.Read(picture, 0, (int)fileStream.Length);
            fileStream.Close();
            return picture;
        }
    }
}
