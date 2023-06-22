using System.Text.Json.Serialization;

namespace Blog.Application.DTOs
{
    /// <summary>
    /// GET Comments: item for CommentResponseDTO as a response object.
    /// </summary>
    public class CommentItemResponseDTO
    {
        public CommentItemResponseDTO(string content, string author, DateTimeOffset timestamp)
        {
            Content = content;
            Author = author;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Content of a comment.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; private set; }

        /// <summary>
        /// Author of a comment.
        /// </summary>
        [JsonPropertyName("author")]
        public string Author { get; private set; }

        /// <summary>
        /// Date and time of a comment.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; private set; }
    }
}
