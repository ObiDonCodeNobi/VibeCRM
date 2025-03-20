using FluentValidation;

namespace VibeCRM.Application.Features.Company.Queries.GetCompanyById
{
    /// <summary>
    /// Validator for the GetCompanyByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetCompanyByIdQueryValidator class with validation rules.
        /// </summary>
        public GetCompanyByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}