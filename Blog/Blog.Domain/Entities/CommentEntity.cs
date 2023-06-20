namespace Blog.Domain.Entities
{
    public class CommentEntity
    {
        public Guid? IdComment { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset StoredAt { get; set; }
        public int PageCount { get; set; }
    }
}
