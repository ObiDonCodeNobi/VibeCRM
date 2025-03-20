using FluentValidation;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;

namespace VibeCRM.Application.Features.InvoiceStatus.Validators
{
    /// <summary>
    /// Validator for the InvoiceStatusListDto
    /// </summary>
    public class InvoiceStatusListDtoValidator : AbstractValidator<InvoiceStatusListDto>
    {
        /// <summary>
        /// Initializes a new instance of the InvoiceStatusListDtoValidator class
        /// </summary>
        public InvoiceStatusListDtoValidator()
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

            RuleFor(x => x.InvoiceCount)
                .GreaterThanOrEqualTo(0).WithMessage("Invoice count must be a non-negative number");
        }
    }
}