using Blog.Application.DTOs;
using Blog.Application.Helpers;
using Blog.Application.Interfaces;
using Blog.Domain.Interfaces.Integration;
using Blog.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ILogger<CommentService> _logger;
        private readonly ICommentRepository _commentRepository;
        private readonly IMessageSender _messageSender;

        public CommentService(ILogger<CommentService> logger, ICommentRepository commentRepository, IMessageSender messageSender)
        {
            _logger = logger;
            _commentRepository = commentRepository;
            _messageSender = messageSender;
        }

        public async Task<CommentResponseDTO> GetComment(int page_size, int page_number, string? author)
        {
            _logger.LogInformation("[CommentService.GetComment] Page size: {0}, Page number: {1}, Author: {2}", page_size, page_number, author ?? "-");

            var result = await _commentRepository.Get(page_size, page_number, author);

            var comments = new CommentResponseDTO(page_size, page_number);

            comments.AddItems(result);

            return comments;
        }

        public void SendComment(CommentRequestDTO request)
        {
            var comment = new CommentMessageDTO(request.Content, request.Author);

            _logger.LogInformation("[CommentService.SendComment] Comment: {0}", comment.ToJsonString());

            var messageBytes = comment.ToByteArray();

            _messageSender.SendMessage(messageBytes, "Comments");
        }
    }
}
