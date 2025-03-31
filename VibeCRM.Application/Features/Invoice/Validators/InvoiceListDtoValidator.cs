using FluentValidation;
using VibeCRM.Shared.DTOs.Invoice;

namespace VibeCRM.Application.Features.Invoice.Validators
{
    /// <summary>
    /// Validator for the InvoiceListDto class
    /// </summary>
    public class InvoiceListDtoValidator : AbstractValidator<InvoiceListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceListDtoValidator"/> class
        /// with validation rules for the InvoiceListDto
        /// </summary>
        public InvoiceListDtoValidator()
        {
            Include(new InvoiceDtoValidator());

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Creation date is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modification date is required");
        }
    }
}