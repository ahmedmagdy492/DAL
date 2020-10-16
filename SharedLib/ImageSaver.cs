using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public static class ImageSaver
    {
        public static string SaveImage(string fileName)
        {
            try
            {
                var bytes = File.ReadAllBytes(fileName);
                string fullPath = Path.Combine(Strings.ImagesServerPath, Guid.NewGuid().ToString("N") + Path.GetFileName(fileName));
                File.WriteAllBytes(fullPath, bytes);
                return fullPath;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetFileName(string fullPath)
        {
            string imagePath = SaveImage(fullPath);

            if(imagePath != null)
            {
                return imagePath;
            }
            return null;
        }
    }
}
