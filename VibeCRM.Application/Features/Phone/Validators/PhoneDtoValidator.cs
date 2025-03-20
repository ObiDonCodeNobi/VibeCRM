using FluentValidation;
using VibeCRM.Application.Features.Phone.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.Phone.Validators
{
    /// <summary>
    /// Validator for the PhoneDto class
    /// </summary>
    public class PhoneDtoValidator : AbstractValidator<PhoneDto>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneDtoValidator"/> class
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository for validating phone type references</param>
        public PhoneDtoValidator(IPhoneTypeRepository phoneTypeRepository)
        {
            _phoneTypeRepository = phoneTypeRepository;

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

            RuleFor(p => p.PhoneTypeId)
                .NotEmpty().WithMessage("Phone type is required")
                .MustAsync(async (phoneTypeId, cancellation) =>
                    await _phoneTypeRepository.ExistsAsync(phoneTypeId, cancellation))
                .WithMessage("The specified phone type does not exist");
        }
    }
}