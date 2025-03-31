using FluentValidation;
using VibeCRM.Shared.DTOs.PersonType;

namespace VibeCRM.Application.Features.PersonType.Validators
{
    /// <summary>
    /// Validator for the PersonTypeDto.
    /// Ensures that all required fields are provided and valid.
    /// </summary>
    public class PersonTypeDtoValidator : AbstractValidator<PersonTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the PersonTypeDtoValidator class.
        /// </summary>
        public PersonTypeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Person type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}