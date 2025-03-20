using FluentValidation;
using VibeCRM.Application.Features.Payment.DTOs;

namespace VibeCRM.Application.Features.Payment.Validators
{
    /// <summary>
    /// Validator for the PaymentDto.
    /// Defines validation rules for payment data transfer objects.
    /// </summary>
    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PaymentDtoValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Payment ID is required.");

            RuleFor(p => p.PaymentTypeId)
                .NotEmpty().WithMessage("Payment type is required.");

            RuleFor(p => p.PaymentStatusId)
                .NotEmpty().WithMessage("Payment status is required.");

            RuleFor(p => p.PaymentDate)
                .NotEmpty().WithMessage("Payment date is required.");

            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(p => p.ReferenceNumber)
                .MaximumLength(100).WithMessage("Reference number must not exceed 100 characters.");

            RuleFor(p => p.Notes)
                .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.");
        }
    }
}