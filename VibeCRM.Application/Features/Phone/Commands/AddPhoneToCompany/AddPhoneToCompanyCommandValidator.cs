using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Commands.AddPhoneToCompany
{
    /// <summary>
    /// Validator for the AddPhoneToCompanyCommand
    /// </summary>
    public class AddPhoneToCompanyCommandValidator : AbstractValidator<AddPhoneToCompanyCommand>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ICompanyRepository _companyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPhoneToCompanyCommandValidator"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for validating phone existence</param>
        /// <param name="companyRepository">The company repository for validating company existence</param>
        public AddPhoneToCompanyCommandValidator(IPhoneRepository phoneRepository, ICompanyRepository companyRepository)
        {
            _phoneRepository = phoneRepository;
            _companyRepository = companyRepository;

            RuleFor(p => p.PhoneId)
                .NotEmpty().WithMessage("Phone ID is required")
                .MustAsync(async (id, cancellation) =>
                    await _phoneRepository.ExistsAsync(id, cancellation))
                .WithMessage("The specified phone does not exist");

            RuleFor(p => p.CompanyId)
                .NotEmpty().WithMessage("Company ID is required")
                .MustAsync(async (id, cancellation) =>
                    await _companyRepository.ExistsAsync(id, cancellation))
                .WithMessage("The specified company does not exist");

            RuleFor(p => p)
                .MustAsync(async (command, cancellation) =>
                    !await _phoneRepository.IsPhoneAssociatedWithCompanyAsync(command.PhoneId, command.CompanyId, cancellation))
                .WithMessage("This phone is already associated with the specified company");
        }
    }
}