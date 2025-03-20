using FluentValidation;
using VibeCRM.Application.Features.PersonStatus.DTOs;

namespace VibeCRM.Application.Features.PersonStatus.Validators
{
    /// <summary>
    /// Validator for the PersonStatusDetailsDto.
    /// Defines validation rules for detailed person status DTOs.
    /// </summary>
    public class PersonStatusDetailsDtoValidator : AbstractValidator<PersonStatusDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonStatusDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PersonStatusDetailsDtoValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Person status ID is required.");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name must not exceed 100 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(p => p.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(p => p.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(p => p.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");

            RuleFor(p => p.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(p => p.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}