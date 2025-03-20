using FluentValidation;
using VibeCRM.Application.Features.PaymentStatus.DTOs;

namespace VibeCRM.Application.Features.PaymentStatus.Validators
{
    /// <summary>
    /// Validator for the PaymentStatusListDto.
    /// Defines validation rules for payment status list DTOs.
    /// </summary>
    public class PaymentStatusListDtoValidator : AbstractValidator<PaymentStatusListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentStatusListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PaymentStatusListDtoValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Payment status ID is required.");

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