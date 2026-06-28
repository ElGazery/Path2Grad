namespace Path2Grad.Helper
{
    public class ImageHelper
    {
        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            try
            {
                // Check if the file exists
                if (File.Exists(imagePath))
                {
                    // Read the image file into a byte array
                    return File.ReadAllBytes(imagePath);
                }
                else
                {
                    throw new FileNotFoundException("The specified image file does not exist.");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during conversion
                Console.WriteLine($"Error converting image to byte array: {ex.Message}");
                return null;
            }
        }
    }
}
