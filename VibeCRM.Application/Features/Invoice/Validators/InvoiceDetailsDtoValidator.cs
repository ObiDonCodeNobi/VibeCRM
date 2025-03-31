using FluentValidation;
using VibeCRM.Shared.DTOs.Invoice;

namespace VibeCRM.Application.Features.Invoice.Validators
{
    /// <summary>
    /// Validator for the InvoiceDetailsDto class
    /// </summary>
    public class InvoiceDetailsDtoValidator : AbstractValidator<InvoiceDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceDetailsDtoValidator"/> class
        /// with validation rules for the InvoiceDetailsDto
        /// </summary>
        public InvoiceDetailsDtoValidator()
        {
            Include(new InvoiceDtoValidator());

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created by user ID is required");

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Creation date is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified by user ID is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modification date is required");
        }
    }
}