using FluentValidation;

namespace VibeCRM.Application.Features.InvoiceStatus.Commands.DeleteInvoiceStatus
{
    /// <summary>
    /// Validator for the DeleteInvoiceStatusCommand
    /// </summary>
    public class DeleteInvoiceStatusCommandValidator : AbstractValidator<DeleteInvoiceStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteInvoiceStatusCommandValidator class
        /// </summary>
        public DeleteInvoiceStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
