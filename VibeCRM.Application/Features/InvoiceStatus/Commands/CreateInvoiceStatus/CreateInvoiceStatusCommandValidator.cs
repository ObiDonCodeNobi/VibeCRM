using FluentValidation;

namespace VibeCRM.Application.Features.InvoiceStatus.Commands.CreateInvoiceStatus
{
    /// <summary>
    /// Validator for the CreateInvoiceStatusCommand
    /// </summary>
    public class CreateInvoiceStatusCommandValidator : AbstractValidator<CreateInvoiceStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateInvoiceStatusCommandValidator class
        /// </summary>
        public CreateInvoiceStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .MaximumLength(100).WithMessage("Status cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number");
        }
    }
}
