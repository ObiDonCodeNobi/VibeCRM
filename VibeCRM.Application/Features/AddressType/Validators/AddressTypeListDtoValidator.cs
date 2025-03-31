using FluentValidation;
using VibeCRM.Shared.DTOs.AddressType;

namespace VibeCRM.Application.Features.AddressType.Validators
{
    /// <summary>
    /// Validator for the AddressTypeListDto class.
    /// Defines validation rules for address type list properties.
    /// </summary>
    public class AddressTypeListDtoValidator : AbstractValidator<AddressTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressTypeListDtoValidator"/> class.
        /// Configures validation rules for AddressTypeListDto properties.
        /// </summary>
        public AddressTypeListDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Address type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Address type name is required.")
                .MaximumLength(50)
                .WithMessage("Address type name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.AddressCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Address count must be a non-negative number.");
        }
    }
}