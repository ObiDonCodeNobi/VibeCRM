using FluentValidation;
using VibeCRM.Shared.DTOs.PaymentStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Validators
{
    /// <summary>
    /// Validator for the PaymentStatusDto.
    /// Defines validation rules for payment status DTOs.
    /// </summary>
    public class PaymentStatusDtoValidator : AbstractValidator<PaymentStatusDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentStatusDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PaymentStatusDtoValidator()
        {
            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name must not exceed 100 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(p => p.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}