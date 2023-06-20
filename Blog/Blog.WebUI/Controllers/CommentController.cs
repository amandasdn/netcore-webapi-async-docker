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
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;

        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
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
        public async Task<IActionResult> GetComment([FromQuery] CommentFilterRequestDTO request)
        {
            _logger.LogInformation("[CommentController.GetComment] Request: {0}", request.ToJsonString());

            var result = new Result();

            try
            {
                var requestValidator = new CommentFilterValidator();
                var requestValidation = requestValidator.Validate(request);

                if (!requestValidation.IsValid)
                {
                    var errorMessages = requestValidation.Errors.Select(x => x.ErrorMessage).ToList();
                    result.AddErrorMessage(errorMessages);

                    _logger.LogError("[CommentController.GetComment] Response: {0}", result.ToJsonString());

                    return BadRequest(result);
                }

                var data = await _commentService.GetComment(request.PageSize, request.PageNumber, request.Author);

                var resultContent = new Result<CommentResponseDTO>(data);

                _logger.LogInformation("[CommentController.GetComment] Response: {0}", resultContent.ToJsonString());

                return Ok(resultContent);
            }
            catch (Exception ex)
            {
                _logger.LogError("[CommentController.GetComment] Error: {0}, Request: {1}", ex.Message, request);
                result.AddErrorMessage(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        /// <summary>
        /// Submit a comment to a database.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public IActionResult PostComment([FromBody] CommentRequestDTO request)
        {
            _logger.LogInformation("[CommentController.PostComment] Request: {0}", request.ToJsonString());

            var result = new Result();

            try
            {
                var requestValidator = new CommentValidator();
                var requestValidation = requestValidator.Validate(request);

                if (!requestValidation.IsValid)
                {
                    var errorMessages = requestValidation.Errors.Select(x => x.ErrorMessage).ToList();
                    result.AddErrorMessage(errorMessages);

                    _logger.LogError("[CommentController.PostComment] Response: {0}", result.ToJsonString());

                    return BadRequest(result);
                }

                _commentService.SendComment(request);

                _logger.LogInformation("[CommentController.PostComment] Response: {0}", result.ToJsonString());

                return Accepted(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("[CommentController.PostComment] Error: {0}, Request: {1}", ex.Message, request);
                result.AddErrorMessage(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

    }
}