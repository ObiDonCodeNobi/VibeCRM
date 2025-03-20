using FluentValidation;
using VibeCRM.Application.Features.PaymentMethod.Commands.CreatePaymentMethod;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.CreatePaymentMethod
{
    /// <summary>
    /// Validator for the CreatePaymentMethodCommand
    /// </summary>
    public class CreatePaymentMethodCommandValidator : AbstractValidator<CreatePaymentMethodCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreatePaymentMethodCommandValidator class
        /// </summary>
        public CreatePaymentMethodCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required");
        }
    }
}
