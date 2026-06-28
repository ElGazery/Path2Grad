using System;
using System.IO;

namespace Path2Grad.Application.Helpers
{
    public class ImageHelper
    {
        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    return File.ReadAllBytes(imagePath);
                }
                else
                {
                    throw new FileNotFoundException("The specified image file does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting image to byte array: {ex.Message}");
                return null;
            }
        }
    }
}
