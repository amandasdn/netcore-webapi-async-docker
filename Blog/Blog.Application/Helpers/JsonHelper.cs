using System.Text.Json;

namespace Blog.Application.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Transform an object into a JSON string.
        /// </summary>
        public static string ToJsonString<T>(this T obj) => JsonSerializer.Serialize(obj);
    }
}
