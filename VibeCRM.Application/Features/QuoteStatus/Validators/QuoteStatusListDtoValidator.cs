using FluentValidation;
using VibeCRM.Application.Features.QuoteStatus.DTOs;

namespace VibeCRM.Application.Features.QuoteStatus.Validators
{
    /// <summary>
    /// Validator for the QuoteStatusListDto class.
    /// Defines validation rules for quote status list data.
    /// </summary>
    public class QuoteStatusListDtoValidator : AbstractValidator<QuoteStatusListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteStatusListDtoValidator"/> class.
        /// Configures validation rules for QuoteStatusListDto properties.
        /// </summary>
        public QuoteStatusListDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Quote status ID is required.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status name is required.")
                .MaximumLength(50)
                .WithMessage("Status name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.QuoteCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quote count must be a non-negative number.");
        }
    }
}
