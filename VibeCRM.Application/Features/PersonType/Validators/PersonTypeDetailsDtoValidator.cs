using FluentValidation;
using VibeCRM.Application.Features.PersonType.DTOs;

namespace VibeCRM.Application.Features.PersonType.Validators
{
    /// <summary>
    /// Validator for the PersonTypeDetailsDto.
    /// Ensures that all required fields are provided and valid.
    /// </summary>
    public class PersonTypeDetailsDtoValidator : AbstractValidator<PersonTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the PersonTypeDetailsDtoValidator class.
        /// </summary>
        public PersonTypeDetailsDtoValidator()
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

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Creation date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Creation date cannot be in the future.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modification date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Modification date cannot be in the future.")
                .GreaterThanOrEqualTo(x => x.CreatedDate).WithMessage("Modification date cannot be earlier than creation date.");

            RuleFor(x => x.PeopleCount)
                .GreaterThanOrEqualTo(0).WithMessage("People count must be a non-negative number.");
        }
    }
}