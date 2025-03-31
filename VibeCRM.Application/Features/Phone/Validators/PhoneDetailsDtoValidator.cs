using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Validators
{
    /// <summary>
    /// Validator for the PhoneDetailsDto class
    /// </summary>
    public class PhoneDetailsDtoValidator : AbstractValidator<PhoneDetailsDto>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneDetailsDtoValidator"/> class
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository for validating phone type references</param>
        /// <param name="companyRepository">The company repository for validating company references</param>
        /// <param name="personRepository">The person repository for validating person references</param>
        public PhoneDetailsDtoValidator(
            IPhoneTypeRepository phoneTypeRepository,
            ICompanyRepository companyRepository,
            IPersonRepository personRepository)
        {
            _phoneTypeRepository = phoneTypeRepository;
            _companyRepository = companyRepository;
            _personRepository = personRepository;

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

            RuleForEach(p => p.Companies)
                .ChildRules(company =>
                {
                    company.RuleFor(c => c.CompanyId)
                        .NotEmpty().WithMessage("Company ID is required")
                        .MustAsync(async (companyId, cancellation) =>
                            await _companyRepository.ExistsAsync(companyId, cancellation))
                        .WithMessage("The specified company does not exist");
                });

            RuleForEach(p => p.Persons)
                .ChildRules(person =>
                {
                    person.RuleFor(p => p.PersonId)
                        .NotEmpty().WithMessage("Person ID is required")
                        .MustAsync(async (personId, cancellation) =>
                            await _personRepository.ExistsAsync(personId, cancellation))
                        .WithMessage("The specified person does not exist");
                });
        }
    }
}