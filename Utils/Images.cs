using System;
using System.IO;
using System.Windows.Forms;

namespace Utils
{
    public class Images
    {
        public static string SaveImage(string urlImage, string subdirectoryName)
        {
            if (urlImage is null)
            {
                return null;
            }

            string destinationDirectory = Path.Combine(Application.StartupPath, "Images", subdirectoryName);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            string destinationPath = Path.Combine(destinationDirectory, Guid.NewGuid().ToString() + Path.GetExtension(urlImage));
            File.Copy(urlImage, destinationPath);
            return destinationPath;
        }
    }
}
