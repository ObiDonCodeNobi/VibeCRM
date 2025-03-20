using FluentValidation;
using VibeCRM.Application.Features.Invoice.DTOs;

namespace VibeCRM.Application.Features.Invoice.Validators
{
    /// <summary>
    /// Validator for the InvoiceDto class
    /// </summary>
    public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceDtoValidator"/> class
        /// with validation rules for the InvoiceDto
        /// </summary>
        public InvoiceDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Invoice ID is required");

            RuleFor(x => x.Number)
                .NotEmpty()
                .WithMessage("Invoice number is required")
                .MaximumLength(50)
                .WithMessage("Invoice number cannot exceed 50 characters");
        }
    }
}