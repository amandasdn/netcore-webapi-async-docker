using Blog.Application.DTOs;
using Blog.Application.Interfaces;

namespace Blog.WebUI.IntegrationTests.Mocks
{
    public class CommentServiceMock : ICommentService
    {
        public async Task<CommentResponseDTO> GetComment(int page_size, int page_number, string? author)
            => new CommentResponseDTO(page_size, page_number);

        public void SendComment(CommentRequestDTO request) { }
    }
}
