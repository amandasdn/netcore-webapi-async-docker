using Blog.Application.DTOs;

namespace Blog.Application.Interfaces
{
    public interface ICommentService
    {
        /// <summary>
        /// Get all comments of a post - with pagination.
        /// </summary>
        /// <param name="page_size">Quantity of comments per page</param>
        /// <param name="page_number">Number of the current page</param>
        /// <param name="author">Filter comments by author name</param>
        Task<CommentResponseDTO> GetComment(int page_size, int page_number, string? author);
    }
}
