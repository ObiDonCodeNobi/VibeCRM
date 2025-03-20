using FluentValidation;

namespace VibeCRM.Application.Features.Payment.Queries.GetAllPayments
{
    /// <summary>
    /// Validator for the GetAllPaymentsQuery to ensure the query contains valid pagination parameters.
    /// </summary>
    public class GetAllPaymentsQueryValidator : AbstractValidator<GetAllPaymentsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllPaymentsQueryValidator class with validation rules.
        /// </summary>
        public GetAllPaymentsQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}