using FluentValidation;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodByName
{
    /// <summary>
    /// Validator for the GetPaymentMethodByNameQuery
    /// </summary>
    public class GetPaymentMethodByNameQueryValidator : AbstractValidator<GetPaymentMethodByNameQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetPaymentMethodByNameQueryValidator class
        /// </summary>
        public GetPaymentMethodByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Payment method name is required")
                .MaximumLength(100).WithMessage("Payment method name cannot exceed 100 characters");
        }
    }
}