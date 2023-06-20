namespace Blog.Domain.Models.Messages
{
    /// <summary>
    /// Model for sending message to RabbitMQ
    /// </summary>
    public class CommentMessage : BaseMessage
    {
        public CommentMessage(string content, string author)
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
