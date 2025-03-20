using FluentValidation;
using VibeCRM.Application.Features.PersonStatus.DTOs;

namespace VibeCRM.Application.Features.PersonStatus.Validators
{
    /// <summary>
    /// Validator for the PersonStatusDto.
    /// Defines validation rules for person status DTOs.
    /// </summary>
    public class PersonStatusDtoValidator : AbstractValidator<PersonStatusDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonStatusDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PersonStatusDtoValidator()
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
        }
    }
}
