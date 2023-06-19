using Blog.Domain.Entities;
using System.Text.Json.Serialization;

namespace Blog.Application.DTOs
{
    /// <summary>
    /// GET Comments: content for a response object.
    /// </summary>
    public class CommentResponseDTO
    {
        public CommentResponseDTO(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;

            Items = new List<CommentItemResponseDTO>();
        }

        /// <summary>
        /// List of comments.
        /// </summary>
        [JsonPropertyName("items")]
        public List<CommentItemResponseDTO> Items { get; private set; }

        /// <summary>
        /// Number of the current page.
        /// </summary>
        [JsonPropertyName("page_number")]
        public int PageNumber { get; private set; }

        /// <summary>
        /// Quantity of comments per page.
        /// </summary>
        [JsonPropertyName("page_size")]
        public int PageSize { get; private set; }

        /// <summary>
        /// Total number of pages.
        /// </summary>
        [JsonPropertyName("page_count")]
        public int PageCount { get; private set; }

        /// <summary>
        /// Entities into CommentItem DTO and add to list.
        /// </summary>
        public void AddItems(IEnumerable<Comment> entities)
        {
            if (entities == null || !entities.Any()) return;

            Items.AddRange(entities.Select(x => new CommentItemResponseDTO(x.Content, x.Author, x.CreatedAt)));

            PageCount = entities.FirstOrDefault().PageCount;
        }
    }
}
