namespace Blog.Domain.Models.Messages
{
    public class BaseMessage
    {
        public BaseMessage()
        {
            Id = Guid.NewGuid();
            CreatedAt = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));
        }

        public Guid Id { get; set; } 
        public DateTimeOffset CreatedAt { get; set; }
    }
}
