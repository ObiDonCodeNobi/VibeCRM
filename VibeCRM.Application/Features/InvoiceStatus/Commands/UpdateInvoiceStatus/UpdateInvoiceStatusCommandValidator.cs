using FluentValidation;

namespace VibeCRM.Application.Features.InvoiceStatus.Commands.UpdateInvoiceStatus
{
    /// <summary>
    /// Validator for the UpdateInvoiceStatusCommand
    /// </summary>
    public class UpdateInvoiceStatusCommandValidator : AbstractValidator<UpdateInvoiceStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateInvoiceStatusCommandValidator class
        /// </summary>
        public UpdateInvoiceStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");

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
