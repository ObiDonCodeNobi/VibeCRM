using FluentValidation;

namespace VibeCRM.Application.Features.Invoice.Queries.GetAllInvoices
{
    /// <summary>
    /// Validator for the GetAllInvoicesQuery to ensure the query contains valid pagination parameters.
    /// </summary>
    public class GetAllInvoicesQueryValidator : AbstractValidator<GetAllInvoicesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllInvoicesQueryValidator class with validation rules.
        /// </summary>
        public GetAllInvoicesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}