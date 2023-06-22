using Blog.Application.DTOs;
using Blog.Application.Services;
using Blog.Application.UnitTests.Mocks;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Integration;
using Blog.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blog.Application.UnitTests.Services
{
    public class CommentServiceTests
    {
        private readonly CommentService _commentService;
        private readonly Mock<ILogger<CommentService>> _logger;
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly Mock<IMessageSender> _messageSender;

        public CommentServiceTests()
        {
            _logger = new Mock<ILogger<CommentService>>();
            _commentRepository = new Mock<ICommentRepository>();
            _messageSender = new Mock<IMessageSender>();

            _commentService = new CommentService(_logger.Object, _commentRepository.Object, _messageSender.Object);
        }

        [Fact]
        public void GetComment_AtFirstPage_DataObtained()
        {
            // Arrange
            int pageSize = 2;
            int pageNumber = 1;
            IEnumerable<CommentEntity> commentEntites = EntityMock.commentEntities.Select(x => { x.PageCount = 3; return x; });
            _commentRepository.Setup(x => x.Get(pageSize, pageNumber, null)).ReturnsAsync(commentEntites);

            // Act
            var result = _commentService.GetComment(pageSize, pageNumber, null).Result;

            // Assert
            _commentRepository.Verify(x => x.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once());
            Assert.Equal(commentEntites.Count(), result.Items.Count);
            Assert.Equal(commentEntites.FirstOrDefault().PageCount, result.PageCount);
        }

        [Fact]
        public void GetComment_AtFirstPage_NoData()
        {
            // Arrange
            int pageSize = 5;
            int pageNumber = 1;
            IEnumerable<CommentEntity> commentEntites = new List<CommentEntity>();
            _commentRepository.Setup(x => x.Get(pageSize, pageNumber, null)).ReturnsAsync(commentEntites);

            // Act
            var result = _commentService.GetComment(pageSize, pageNumber, null).Result;

            // Assert
            _commentRepository.Verify(x => x.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once());
            Assert.Equal(commentEntites.Count(), result.Items.Count);
            Assert.Equal(0, result.PageCount);
            Assert.Empty(result.Items);
        }

        [Fact]
        public void SendComment_CallingMessageSender()
        {
            // Act
            _commentService.SendComment(Mock.Of<CommentRequestDTO>());

            // Assert
            _messageSender.Verify(x => x.SendMessage(It.IsAny<byte[]>(), It.IsAny<string>()), Times.Once());
        }
    }
}