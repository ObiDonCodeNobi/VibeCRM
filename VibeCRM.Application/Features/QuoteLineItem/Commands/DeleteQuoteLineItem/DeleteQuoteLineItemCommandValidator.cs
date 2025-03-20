using FluentValidation;

namespace VibeCRM.Application.Features.QuoteLineItem.Commands.DeleteQuoteLineItem
{
    /// <summary>
    /// Validator for the DeleteQuoteLineItemCommand
    /// </summary>
    public class DeleteQuoteLineItemCommandValidator : AbstractValidator<DeleteQuoteLineItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteQuoteLineItemCommandValidator"/> class
        /// </summary>
        public DeleteQuoteLineItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote line item ID is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required");
        }
    }
}