using FluentValidation;

namespace VibeCRM.Application.Features.Invoice.Commands.UpdateInvoice
{
    /// <summary>
    /// Validator for the UpdateInvoiceCommand
    /// </summary>
    public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInvoiceCommandValidator"/> class
        /// with validation rules for updating an invoice
        /// </summary>
        public UpdateInvoiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Invoice ID is required");

            RuleFor(x => x.Number)
                .NotEmpty()
                .WithMessage("Invoice number is required")
                .MaximumLength(50)
                .WithMessage("Invoice number cannot exceed 50 characters");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified by user ID is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modification date is required");
        }
    }
}