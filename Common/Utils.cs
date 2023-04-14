using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Utils
    {
        public class ImageProperties
        {
            public string FileExtension { get; set; }
            public byte[] ImageBytes { get; set; }
        }

        public static ImageProperties ConvertImageBase64StringToByteArr(string base64ImageString)
        {
            byte[] imageBytes = new byte[] { 0 };
            string fileExtension = "";
            if (!string.IsNullOrEmpty(base64ImageString))
            {
                string[] imageBase64Splitted = base64ImageString.Split(',');
                fileExtension = imageBase64Splitted[0].Split(':')[1].Split(';')[0].Split('/')[1];
                string imageBase64 = imageBase64Splitted[1];
                imageBytes = Convert.FromBase64String(imageBase64);
            }

            return new ImageProperties { FileExtension = fileExtension, ImageBytes = imageBytes };
        }
    }
}
