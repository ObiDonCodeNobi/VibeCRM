using FluentValidation;

namespace VibeCRM.Application.Features.Invoice.Commands.DeleteInvoice
{
    /// <summary>
    /// Validator for the DeleteInvoiceCommand
    /// </summary>
    public class DeleteInvoiceCommandValidator : AbstractValidator<DeleteInvoiceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteInvoiceCommandValidator"/> class
        /// with validation rules for deleting an invoice
        /// </summary>
        public DeleteInvoiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Invoice ID is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified by user ID is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modification date is required");
        }
    }
}