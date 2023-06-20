using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Get all comments of a post - with pagination.
        /// </summary>
        /// <param name="page_size">Quantity of comments per page</param>
        /// <param name="page_number">Number of the current page</param>
        /// <param name="author">Filter comments by author name</param>
        Task<IEnumerable<CommentEntity>> Get(int page_size, int page_number, string? author);
    }
}
