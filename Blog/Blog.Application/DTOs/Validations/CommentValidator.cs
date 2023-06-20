using FluentValidation;

namespace Blog.Application.DTOs.Validations
{
    /// <summary>
    /// Validation for CommentRequestDTO.
    /// </summary>
    public class CommentValidator : AbstractValidator<CommentRequestDTO>
    {
        public CommentValidator()
        {
            // Content is required
            RuleFor(x => x.Content)
                .Length(1, 500)
                .WithMessage($"The field {nameof(CommentRequestDTO.Content)} is required and must not exceed 500 characters.");

            // Author is required
            RuleFor(x => x.Author)
                .Length(1, 500)
                .WithMessage($"The field {nameof(CommentRequestDTO.Author)} is required and must not exceed 500 characters.");
        }
    }
}
