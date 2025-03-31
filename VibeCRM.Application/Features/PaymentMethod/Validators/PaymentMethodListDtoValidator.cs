using FluentValidation;
using VibeCRM.Shared.DTOs.PaymentMethod;

namespace VibeCRM.Application.Features.PaymentMethod.Validators
{
    /// <summary>
    /// Validator for the PaymentMethodListDto
    /// </summary>
    public class PaymentMethodListDtoValidator : AbstractValidator<PaymentMethodListDto>
    {
        /// <summary>
        /// Initializes a new instance of the PaymentMethodListDtoValidator class
        /// </summary>
        public PaymentMethodListDtoValidator()
        {
            RuleFor(x => x.PaymentMethods)
                .NotNull().WithMessage("Payment methods collection cannot be null");

            RuleFor(x => x.TotalCount)
                .GreaterThanOrEqualTo(0).WithMessage("Total count must be a non-negative number");
        }
    }
}