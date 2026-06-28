using System.IO;
using Microsoft.AspNetCore.Http;

namespace Path2Grad.Application.Helpers
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
