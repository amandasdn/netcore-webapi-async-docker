using System.Text.Json.Serialization;

namespace Blog.Application.DTOs
{
    /// <summary>
    /// POST Comments: content as a request object.
    /// </summary>
    public class CommentRequestDTO
    {
        /// <summary>
        /// Content of a comment.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// Author of a comment.
        /// </summary>
        [JsonPropertyName("author")]
        public string Author { get; set; }
    }
}
