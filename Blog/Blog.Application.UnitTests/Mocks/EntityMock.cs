using Blog.Domain.Entities;

namespace Blog.Application.UnitTests.Mocks
{
    public static class EntityMock
    {
        private static DateTimeOffset _currentDateTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));

        public static IEnumerable<CommentEntity> commentEntities = new List<CommentEntity>
        {
            new CommentEntity
            {
                IdComment = Guid.NewGuid(),
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Author = "John",
                CreatedAt = _currentDateTime,
                StoredAt = _currentDateTime
            },
            new CommentEntity
            {
                IdComment = Guid.NewGuid(),
                Content = "Nulla facilisi. In sit amet turpis neque.",
                Author = "Mary",
                CreatedAt = _currentDateTime,
                StoredAt = _currentDateTime
            },
            new CommentEntity
            {
                IdComment = Guid.NewGuid(),
                Content = "Cras eleifend urna eu quam aliquet, nec interdum sem malesuada.",
                Author = "Ana",
                CreatedAt = _currentDateTime,
                StoredAt = _currentDateTime
            },
            new CommentEntity
            {
                IdComment = Guid.NewGuid(),
                Content = "Curabitur eget nibh nibh. Morbi convallis semper nibh eu aliquam.",
                Author = "Patrick",
                CreatedAt = _currentDateTime,
                StoredAt = _currentDateTime
            },
            new CommentEntity
            {
                IdComment = Guid.NewGuid(),
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Author = "John",
                CreatedAt = _currentDateTime,
                StoredAt = _currentDateTime
            }
        };
    }
}
