using FluentValidation;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetAllPaymentLineItems
{
    /// <summary>
    /// Validator for the GetAllPaymentLineItemsQuery to ensure the query contains valid pagination parameters.
    /// </summary>
    public class GetAllPaymentLineItemsQueryValidator : AbstractValidator<GetAllPaymentLineItemsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllPaymentLineItemsQueryValidator class with validation rules.
        /// </summary>
        public GetAllPaymentLineItemsQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}