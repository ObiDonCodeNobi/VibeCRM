using FluentValidation;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodById;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodById
{
    /// <summary>
    /// Validator for the GetPaymentMethodByIdQuery
    /// </summary>
    public class GetPaymentMethodByIdQueryValidator : AbstractValidator<GetPaymentMethodByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetPaymentMethodByIdQueryValidator class
        /// </summary>
        public GetPaymentMethodByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Payment method ID is required");
        }
    }
}
