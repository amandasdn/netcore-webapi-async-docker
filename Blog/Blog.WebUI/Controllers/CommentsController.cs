using Blog.Application.DTOs;
using Blog.Application.DTOs.Validations;
using Blog.Application.Helpers;
using Blog.Application.Interfaces;
using Blog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly ICommentService _commentService;

        public CommentsController(ILogger<CommentsController> logger, ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }

        /// <summary>
        /// Get all comments of a post - with pagination.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Result<CommentResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComments([FromQuery] CommentFilterRequestDTO request)
        {
            _logger.LogInformation("[CommentsController.GetComments] Request: {0}", request.ToJsonString());

            var result = new Result();

            try
            {
                var requestValidator = new CommentFilterValidator();
                var requestValidation = requestValidator.Validate(request);

                if (!requestValidation.IsValid)
                {
                    var errorMessages = requestValidation.Errors.Select(x => x.ErrorMessage).ToList();
                    result.AddErrorMessage(errorMessages);

                    _logger.LogError("[GetComments] Response: {0}", result.ToJsonString());

                    return BadRequest(result);
                }

                var data = await _commentService.GetComment(request.PageSize, request.PageNumber, request.Author);

                var resultContent = new Result<CommentResponseDTO>(data);

                _logger.LogInformation("[GetComments] Response: {0}", resultContent.ToJsonString());

                return Ok(resultContent);
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
    }
}