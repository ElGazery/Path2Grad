namespace Path2Grad.Helper
{
    public static class IFormToByteHelper
    {
        public static byte[] ConvertToBytes(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                 file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
