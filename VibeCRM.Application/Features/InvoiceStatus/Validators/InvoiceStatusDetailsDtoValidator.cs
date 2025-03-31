using FluentValidation;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Validators
{
    /// <summary>
    /// Validator for the InvoiceStatusDetailsDto
    /// </summary>
    public class InvoiceStatusDetailsDtoValidator : AbstractValidator<InvoiceStatusDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the InvoiceStatusDetailsDtoValidator class
        /// </summary>
        public InvoiceStatusDetailsDtoValidator()
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

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required");
        }
    }
}