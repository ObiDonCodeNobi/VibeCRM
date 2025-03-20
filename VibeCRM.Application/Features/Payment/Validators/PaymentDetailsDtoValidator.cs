using FluentValidation;
using VibeCRM.Application.Features.Payment.DTOs;

namespace VibeCRM.Application.Features.Payment.Validators
{
    /// <summary>
    /// Validator for the PaymentDetailsDto.
    /// Extends the base PaymentDtoValidator with additional validation rules for detailed payment information.
    /// </summary>
    public class PaymentDetailsDtoValidator : AbstractValidator<PaymentDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PaymentDetailsDtoValidator()
        {
            // Include all validation rules from the base PaymentDto validator
            Include(new PaymentDtoValidator());

            RuleFor(p => p.PaymentTypeName)
                .MaximumLength(100).WithMessage("Payment type name must not exceed 100 characters.");

            RuleFor(p => p.PaymentMethodTypeName)
                .MaximumLength(100).WithMessage("Payment method type name must not exceed 100 characters.");

            RuleFor(p => p.PaymentStatusName)
                .MaximumLength(100).WithMessage("Payment status name must not exceed 100 characters.");

            RuleFor(p => p.InvoiceNumber)
                .MaximumLength(50).WithMessage("Invoice number must not exceed 50 characters.");

            RuleFor(p => p.CompanyName)
                .MaximumLength(200).WithMessage("Company name must not exceed 200 characters.");

            RuleFor(p => p.PersonName)
                .MaximumLength(200).WithMessage("Person name must not exceed 200 characters.");

            RuleFor(p => p.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(p => p.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(p => p.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");

            RuleFor(p => p.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}