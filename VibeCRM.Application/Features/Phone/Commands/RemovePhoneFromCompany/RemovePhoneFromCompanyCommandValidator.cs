using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Commands.RemovePhoneFromCompany
{
    /// <summary>
    /// Validator for the RemovePhoneFromCompanyCommand
    /// </summary>
    public class RemovePhoneFromCompanyCommandValidator : AbstractValidator<RemovePhoneFromCompanyCommand>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ICompanyRepository _companyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePhoneFromCompanyCommandValidator"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for validating phone existence and association</param>
        /// <param name="companyRepository">The company repository for validating company existence</param>
        public RemovePhoneFromCompanyCommandValidator(IPhoneRepository phoneRepository, ICompanyRepository companyRepository)
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
                    await _phoneRepository.IsPhoneAssociatedWithCompanyAsync(command.PhoneId, command.CompanyId, cancellation))
                .WithMessage("This phone is not associated with the specified company");
        }
    }
}