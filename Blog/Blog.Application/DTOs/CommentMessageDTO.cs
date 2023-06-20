using Blog.Domain.Models.Messages;

namespace Blog.Application.DTOs
{
    /// <summary>
    /// DTO for sending message to RabbitMQ
    /// </summary>
    public class CommentMessageDTO : BaseMessage
    {
        public CommentMessageDTO(string content, string author)
        {
            Content = content;
            Author = author;
        }

        /// <summary>
        /// Content of a comment.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Author of a comment.
        /// </summary>
        public string Author { get; set; }
    }
}
