using FluentValidation;
using VibeCRM.Shared.DTOs.QuoteStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Validators
{
    /// <summary>
    /// Validator for the QuoteStatusDto class.
    /// Defines validation rules for quote status data.
    /// </summary>
    public class QuoteStatusDtoValidator : AbstractValidator<QuoteStatusDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteStatusDtoValidator"/> class.
        /// Configures validation rules for QuoteStatusDto properties.
        /// </summary>
        public QuoteStatusDtoValidator()
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
        }
    }
}