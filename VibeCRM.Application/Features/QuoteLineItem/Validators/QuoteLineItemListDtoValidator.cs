using FluentValidation;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the QuoteLineItemListDto
    /// </summary>
    public class QuoteLineItemListDtoValidator : AbstractValidator<QuoteLineItemListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteLineItemListDtoValidator"/> class
        /// </summary>
        public QuoteLineItemListDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required");

            RuleFor(x => x.QuoteId)
                .NotEmpty().WithMessage("Quote ID is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero");

            RuleFor(x => x.LineNumber)
                .GreaterThan(0).WithMessage("Line number must be greater than zero");

            RuleFor(x => x.ExtendedPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Extended price must be greater than or equal to zero");
        }
    }
}