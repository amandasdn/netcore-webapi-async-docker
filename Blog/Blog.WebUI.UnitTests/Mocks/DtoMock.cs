using Blog.Application.DTOs;
using Blog.Domain.Entities;
using Moq;

namespace Blog.WebUI.UnitTests.Mocks
{
    public static class DtoMock
    {
        private static DateTimeOffset _currentDateTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));
        
        public static CommentResponseDTO GetCommentResponseDto(int pageSize, int pageNumber, string? author)
        {
            var result = new CommentResponseDTO(pageSize, pageNumber);

            var entities = new List<CommentEntity>
            {
                new CommentEntity
                {
                    IdComment = Guid.NewGuid(),
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Author = "John",
                    CreatedAt = _currentDateTime,
                    StoredAt = _currentDateTime
                }
            };

            result.AddItems(entities);

            return result;
        }

        public static CommentResponseDTO GetCommentResponseDtoNoData(int pageSize, int pageNumber, string? author)
        {
            var result = new CommentResponseDTO(pageSize, pageNumber);

            var entities = new List<CommentEntity>();

            result.AddItems(entities);

            return result;
        }

        public static CommentRequestDTO commentRequestDto = new()
        {
            Author = "Peter",
            Content = "Cras eleifend urna eu quam aliquet, nec interdum sem malesuada."
        };
    }
}
