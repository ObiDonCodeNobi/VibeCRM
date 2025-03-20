using FluentValidation;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.UpdatePaymentMethod
{
    /// <summary>
    /// Validator for the UpdatePaymentMethodCommand
    /// </summary>
    public class UpdatePaymentMethodCommandValidator : AbstractValidator<UpdatePaymentMethodCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdatePaymentMethodCommandValidator class
        /// </summary>
        public UpdatePaymentMethodCommandValidator()
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

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required");
        }
    }
}