using FluentValidation;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;

namespace VibeCRM.Application.Features.PaymentLineItem.Validators
{
    /// <summary>
    /// Validator for the PaymentLineItemDetailsDto class.
    /// </summary>
    /// <remarks>
    /// Extends the base PaymentLineItemDtoValidator with additional validation rules for detailed views.
    /// </remarks>
    public class PaymentLineItemDetailsDtoValidator : PaymentLineItemDtoValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentLineItemDetailsDtoValidator"/> class.
        /// </summary>
        public PaymentLineItemDetailsDtoValidator()
        {
            // Cast to PaymentLineItemDetailsDto to access the properties specific to this DTO
            RuleFor(x => ((PaymentLineItemDetailsDto)x).Notes)
                .MaximumLength(2000).WithMessage("Notes cannot exceed 2000 characters.");

            RuleFor(x => ((PaymentLineItemDetailsDto)x).CreatedBy)
                .NotEmpty().WithMessage("Created by is required.")
                .MaximumLength(100).WithMessage("Created by cannot exceed 100 characters.");

            RuleFor(x => ((PaymentLineItemDetailsDto)x).ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.")
                .MaximumLength(100).WithMessage("Modified by cannot exceed 100 characters.");

            RuleFor(x => ((PaymentLineItemDetailsDto)x).CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(x => ((PaymentLineItemDetailsDto)x).ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}