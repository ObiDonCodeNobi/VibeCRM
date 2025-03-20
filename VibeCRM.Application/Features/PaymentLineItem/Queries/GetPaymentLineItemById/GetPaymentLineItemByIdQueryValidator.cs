using FluentValidation;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemById
{
    /// <summary>
    /// Validator for the GetPaymentLineItemByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetPaymentLineItemByIdQueryValidator : AbstractValidator<GetPaymentLineItemByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetPaymentLineItemByIdQueryValidator class with validation rules.
        /// </summary>
        public GetPaymentLineItemByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}