using FluentValidation;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Validators
{
    /// <summary>
    /// Validator for the PaymentListDto.
    /// Extends the base PaymentDtoValidator with additional validation rules for list display payment information.
    /// </summary>
    public class PaymentListDtoValidator : AbstractValidator<PaymentListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PaymentListDtoValidator()
        {
            // Include all validation rules from the base PaymentDto validator
            Include(new PaymentDtoValidator());

            RuleFor(p => p.PaymentTypeName)
                .MaximumLength(100).WithMessage("Payment type name must not exceed 100 characters.");

            RuleFor(p => p.PaymentStatusName)
                .MaximumLength(100).WithMessage("Payment status name must not exceed 100 characters.");

            RuleFor(p => p.InvoiceNumber)
                .MaximumLength(50).WithMessage("Invoice number must not exceed 50 characters.");

            RuleFor(p => p.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");
        }
    }
}