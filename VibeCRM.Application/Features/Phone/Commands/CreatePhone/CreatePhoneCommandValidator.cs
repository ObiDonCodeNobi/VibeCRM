using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.Phone.Commands.CreatePhone
{
    /// <summary>
    /// Validator for the CreatePhoneCommand
    /// </summary>
    public class CreatePhoneCommandValidator : AbstractValidator<CreatePhoneCommand>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IPhoneTypeRepository _phoneTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePhoneCommandValidator"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for validating phone number uniqueness</param>
        /// <param name="phoneTypeRepository">The phone type repository for validating phone type references</param>
        public CreatePhoneCommandValidator(IPhoneRepository phoneRepository, IPhoneTypeRepository phoneTypeRepository)
        {
            _phoneRepository = phoneRepository;
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

            RuleFor(p => p)
                .MustAsync(async (command, cancellation) =>
                {
                    string formattedNumber = $"{command.AreaCode}{command.Prefix}{command.LineNumber}";
                    return await _phoneRepository.IsPhoneNumberUniqueAsync(formattedNumber, cancellation);
                })
                .WithMessage("A phone with this number already exists");
        }
    }
}