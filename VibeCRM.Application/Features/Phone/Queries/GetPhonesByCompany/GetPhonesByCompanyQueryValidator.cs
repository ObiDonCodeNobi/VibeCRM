using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByCompany
{
    /// <summary>
    /// Validator for the GetPhonesByCompanyQuery
    /// </summary>
    public class GetPhonesByCompanyQueryValidator : AbstractValidator<GetPhonesByCompanyQuery>
    {
        private readonly ICompanyRepository _companyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhonesByCompanyQueryValidator"/> class
        /// </summary>
        /// <param name="companyRepository">The company repository for validating company existence</param>
        public GetPhonesByCompanyQueryValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;

            RuleFor(p => p.CompanyId)
                .NotEmpty().WithMessage("Company ID is required")
                .MustAsync(async (id, cancellation) =>
                    await _companyRepository.ExistsAsync(id, cancellation))
                .WithMessage("The specified company does not exist");
        }
    }
}