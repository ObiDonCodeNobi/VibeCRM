using FluentValidation;

namespace VibeCRM.Application.Features.Invoice.Commands.CreateInvoice
{
    /// <summary>
    /// Validator for the CreateInvoiceCommand
    /// </summary>
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvoiceCommandValidator"/> class
        /// with validation rules for creating a new invoice
        /// </summary>
        public CreateInvoiceCommandValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .WithMessage("Invoice number is required")
                .MaximumLength(50)
                .WithMessage("Invoice number cannot exceed 50 characters");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created by user ID is required");

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Creation date is required");
        }
    }
}