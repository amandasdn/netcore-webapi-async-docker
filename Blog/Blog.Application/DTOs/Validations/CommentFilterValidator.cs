using FluentValidation;

namespace Blog.Application.DTOs.Validations
{
    /// <summary>
    /// Validation for CommentFilterRequestDTO.
    /// </summary>
    public class CommentFilterValidator : AbstractValidator<CommentFilterRequestDTO>
    {
        public CommentFilterValidator()
        {
            // PageSize is required and should be greater than 0
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage($"The field {nameof(CommentFilterRequestDTO.PageSize)} should be greater than 0.");

            // PageNumber is required and should be greater than 0
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage($"The field {nameof(CommentFilterRequestDTO.PageNumber)} should be greater than 0.");
        }
    }
}
