using FluentValidation;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;

namespace VibeCRM.Application.Features.PaymentLineItem.Validators
{
    /// <summary>
    /// Validator for the PaymentLineItemListDto class.
    /// </summary>
    /// <remarks>
    /// Extends the base PaymentLineItemDtoValidator with additional validation rules for list views.
    /// </remarks>
    public class PaymentLineItemListDtoValidator : PaymentLineItemDtoValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentLineItemListDtoValidator"/> class.
        /// </summary>
        public PaymentLineItemListDtoValidator()
        {
            // Cast to PaymentLineItemListDto to access the properties specific to this DTO
            RuleFor(x => ((PaymentLineItemListDto)x).CreatedBy)
                .NotEmpty().WithMessage("Created by is required.")
                .MaximumLength(100).WithMessage("Created by cannot exceed 100 characters.");

            RuleFor(x => ((PaymentLineItemListDto)x).CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");
        }
    }
}