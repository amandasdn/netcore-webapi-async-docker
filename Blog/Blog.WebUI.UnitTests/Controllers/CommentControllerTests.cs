using Blog.Application.DTOs;
using Blog.Application.Interfaces;
using Blog.Domain.Models;
using Blog.WebUI.Controllers;
using Blog.WebUI.UnitTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blog.WebUI.UnitTests.Controllers
{
    public class CommentControllerTests
    {
        private readonly CommentController _commentController;
        private readonly Mock<ILogger<CommentController>> _logger;
        private readonly Mock<ICommentService> _commentService;

        public CommentControllerTests()
        {
            _logger = new Mock<ILogger<CommentController>>();
            _commentService = new Mock<ICommentService>();

            _commentController = new CommentController(_logger.Object, _commentService.Object);
        }

        [Fact]
        public void GetComment_WithoutFilter_BadRequest()
        {
            // Arrange
            CommentFilterRequestDTO filter = new();

            // Act
            var result = _commentController.GetComment(filter).Result;

            // Assert
            _commentService.Verify(x => x.GetComment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<Result>(((BadRequestObjectResult)result).Value);
            Assert.False((((BadRequestObjectResult)result).Value as Result).Success);
            Assert.NotEmpty((((BadRequestObjectResult)result).Value as Result).Errors);
        }

        [Fact]
        public void GetComment_WithFilter_Success()
        {
            // Arrange
            CommentFilterRequestDTO filter = new() { PageSize = 5, PageNumber = 1 };
            _commentService.Setup(x => x.GetComment(filter.PageSize, filter.PageNumber, It.IsAny<string>()))
                .ReturnsAsync(DtoMock.GetCommentResponseDto(filter.PageSize, filter.PageNumber, null));

            // Act
            var result = _commentController.GetComment(filter).Result;

            // Assert
            _commentService.Verify(x => x.GetComment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Result<CommentResponseDTO>>(((OkObjectResult)result).Value);
            Assert.True((((OkObjectResult)result).Value as Result<CommentResponseDTO>).Success);
            Assert.Null((((OkObjectResult)result).Value as Result<CommentResponseDTO>).Errors);
            Assert.NotNull((((OkObjectResult)result).Value as Result<CommentResponseDTO>).Content);
            Assert.NotEmpty((((OkObjectResult)result).Value as Result<CommentResponseDTO>).Content.Items);
        }

        [Fact]
        public void GetComment_WithFilter_SuccessNoData()
        {
            // Arrange
            CommentFilterRequestDTO filter = new() { PageSize = 5, PageNumber = 1 };
            _commentService.Setup(x => x.GetComment(filter.PageSize, filter.PageNumber, It.IsAny<string>()))
                .ReturnsAsync(DtoMock.GetCommentResponseDtoNoData(filter.PageSize, filter.PageNumber, null));

            // Act
            var result = _commentController.GetComment(filter).Result;

            // Assert
            _commentService.Verify(x => x.GetComment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Result<CommentResponseDTO>>(((OkObjectResult)result).Value);
            Assert.True((((OkObjectResult)result).Value as Result<CommentResponseDTO>).Success);
            Assert.Null((((OkObjectResult)result).Value as Result<CommentResponseDTO>).Errors);
            Assert.NotNull((((OkObjectResult)result).Value as Result<CommentResponseDTO>).Content);
            Assert.Empty((((OkObjectResult)result).Value as Result<CommentResponseDTO>).Content.Items);
        }

        [Fact]
        public void GetComment_WithFilterWithException_InternalServerError()
        {
            // Arrange
            CommentFilterRequestDTO filter = new() { PageSize = 5, PageNumber = 1 };
            _commentService.Setup(x => x.GetComment(filter.PageSize, filter.PageNumber, It.IsAny<string>()))
                .Throws(new Exception());

            // Act
            var result = _commentController.GetComment(filter).Result;

            // Assert
            _commentService.Verify(x => x.GetComment(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
            Assert.IsType<Result>(((ObjectResult)result).Value);
            Assert.False((((ObjectResult)result).Value as Result).Success);
            Assert.NotEmpty((((ObjectResult)result).Value as Result).Errors);
        }

        [Fact]
        public void PostComment_WithContent_Accepted()
        {
            // Arrange
            CommentRequestDTO commentRequestDTO = DtoMock.commentRequestDto;
            _commentService.Setup(x => x.SendComment(commentRequestDTO));

            // Act
            var result = _commentController.PostComment(commentRequestDTO);

            // Assert
            _commentService.Verify(x => x.SendComment(commentRequestDTO), Times.Once);
            Assert.IsType<AcceptedResult>(result);
            Assert.IsType<Result>(((AcceptedResult)result).Value);
            Assert.True((((AcceptedResult)result).Value as Result).Success);
            Assert.Null((((AcceptedResult)result).Value as Result).Errors);
        }

        [Fact]
        public void PostComment_WithContent_BadRequest()
        {
            // Arrange
            CommentRequestDTO commentRequestDTO = new();

            // Act
            var result = _commentController.PostComment(commentRequestDTO);

            // Assert
            _commentService.Verify(x => x.SendComment(commentRequestDTO), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<Result>(((BadRequestObjectResult)result).Value);
            Assert.False((((BadRequestObjectResult)result).Value as Result).Success);
            Assert.NotEmpty((((BadRequestObjectResult)result).Value as Result).Errors);
        }

        [Fact]
        public void PostComment_WithContentWithException_InternalServerError()
        {
            // Arrange
            CommentRequestDTO commentRequestDTO = DtoMock.commentRequestDto;
            _commentService.Setup(x => x.SendComment(commentRequestDTO)).Throws(new Exception());

            // Act
            var result = _commentController.PostComment(commentRequestDTO);

            // Assert
            _commentService.Verify(x => x.SendComment(commentRequestDTO), Times.Once);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
            Assert.IsType<Result>(((ObjectResult)result).Value);
            Assert.False((((ObjectResult)result).Value as Result).Success);
            Assert.NotEmpty((((ObjectResult)result).Value as Result).Errors);
        }
    }
}