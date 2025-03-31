using FluentValidation;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Validators
{
    /// <summary>
    /// Validator for the InvoiceStatusDto
    /// </summary>
    public class InvoiceStatusDtoValidator : AbstractValidator<InvoiceStatusDto>
    {
        /// <summary>
        /// Initializes a new instance of the InvoiceStatusDtoValidator class
        /// </summary>
        public InvoiceStatusDtoValidator()
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