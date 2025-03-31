using FluentValidation;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Validators
{
    /// <summary>
    /// Validator for the PhoneListDto class
    /// </summary>
    public class PhoneListDtoValidator : AbstractValidator<PhoneListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneListDtoValidator"/> class
        /// </summary>
        public PhoneListDtoValidator()
        {
            RuleFor(p => p.AreaCode)
                .NotEmpty().WithMessage("Area code is required")
                .InclusiveBetween(100, 999).WithMessage("Area code must be a 3-digit number");

            RuleFor(p => p.Prefix)
                .NotEmpty().WithMessage("Prefix is required")
                .InclusiveBetween(100, 999).WithMessage("Prefix must be a 3-digit number");

            RuleFor(p => p.LineNumber)
                .NotEmpty().WithMessage("Line number is required")
                .InclusiveBetween(1000, 9999).WithMessage("Line number must be a 4-digit number");

            RuleFor(p => p.Extension)
                .InclusiveBetween(0, 99999).WithMessage("Extension must be between 0 and 99999")
                .When(p => p.Extension.HasValue);

            RuleFor(p => p.PhoneTypeName)
                .NotEmpty().WithMessage("Phone type name is required");
        }
    }
}