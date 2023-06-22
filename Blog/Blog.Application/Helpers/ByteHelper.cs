using System.Text;
using System.Text.Json;

namespace Blog.Application.Helpers
{
    public static class ByteHelper
    {
        /// <summary>
        /// Transform an object into a byte array.
        /// </summary>
        public static byte[] ToByteArray<T>(this T obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<T>((T)obj, options);
            var body = Encoding.UTF8.GetBytes(json);

            return body;
        }
    }
}
