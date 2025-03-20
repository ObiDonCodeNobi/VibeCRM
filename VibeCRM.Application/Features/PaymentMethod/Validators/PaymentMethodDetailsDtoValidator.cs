using FluentValidation;
using VibeCRM.Application.Features.PaymentMethod.DTOs;

namespace VibeCRM.Application.Features.PaymentMethod.Validators
{
    /// <summary>
    /// Validator for the PaymentMethodDetailsDto
    /// </summary>
    public class PaymentMethodDetailsDtoValidator : AbstractValidator<PaymentMethodDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the PaymentMethodDetailsDtoValidator class
        /// </summary>
        public PaymentMethodDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Payment method ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required");
        }
    }
}
