using FluentValidation;
using VibeCRM.Shared.DTOs.QuoteStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Validators
{
    /// <summary>
    /// Validator for the QuoteStatusDetailsDto class.
    /// Defines validation rules for detailed quote status data.
    /// </summary>
    public class QuoteStatusDetailsDtoValidator : AbstractValidator<QuoteStatusDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteStatusDetailsDtoValidator"/> class.
        /// Configures validation rules for QuoteStatusDetailsDto properties.
        /// </summary>
        public QuoteStatusDetailsDtoValidator()
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

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Created date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created by is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modified date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified by is required.");
        }
    }
}