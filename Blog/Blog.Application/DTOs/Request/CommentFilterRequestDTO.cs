using Microsoft.AspNetCore.Mvc;

namespace Blog.Application.DTOs
{
    /// <summary>
    /// GET Comments: filter as a request object.
    /// </summary>
    public class CommentFilterRequestDTO
    {
        /// <summary>
        /// Quantity of comments per page.
        /// </summary>
        [BindProperty(Name = "page_size")]
        public int PageSize { get; set; }

        /// <summary>
        /// Number of the current page.
        /// </summary>
        [BindProperty(Name = "page_number")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Filter comments by author name.
        /// </summary>
        [BindProperty(Name = "author")]
        public string? Author { get; set; }
    }
}
