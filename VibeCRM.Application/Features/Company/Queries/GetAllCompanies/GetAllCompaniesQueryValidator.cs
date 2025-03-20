using FluentValidation;

namespace VibeCRM.Application.Features.Company.Queries.GetAllCompanies
{
    /// <summary>
    /// Validator for the GetAllCompaniesQuery to ensure the query contains valid pagination parameters.
    /// </summary>
    public class GetAllCompaniesQueryValidator : AbstractValidator<GetAllCompaniesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllCompaniesQueryValidator class with validation rules.
        /// </summary>
        public GetAllCompaniesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}