using FluentValidation;

namespace VibeCRM.Application.Features.Invoice.DTOs
{
    /// <summary>
    /// Validator for the InvoiceLineItemDto to ensure it contains valid data.
    /// </summary>
    public class InvoiceLineItemDtoValidator : AbstractValidator<InvoiceLineItemDto>
    {
        /// <summary>
        /// Initializes a new instance of the InvoiceLineItemDtoValidator class with validation rules.
        /// </summary>
        public InvoiceLineItemDtoValidator()
        {
            RuleFor(dto => dto.InvoiceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

            RuleFor(dto => dto.ServiceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

            RuleFor(dto => dto.ServiceName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(dto => dto.Description)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(dto => dto.Quantity)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(dto => dto.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(dto => dto.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");

            RuleFor(dto => dto.Tax)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0.");
        }
    }
}