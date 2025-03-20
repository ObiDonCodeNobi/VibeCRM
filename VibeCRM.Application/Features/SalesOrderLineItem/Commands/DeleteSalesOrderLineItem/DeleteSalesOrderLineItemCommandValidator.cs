using FluentValidation;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.DeleteSalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Validators
{
    /// <summary>
    /// Validator for the DeleteSalesOrderLineItemCommand
    /// </summary>
    public class DeleteSalesOrderLineItemCommandValidator : AbstractValidator<DeleteSalesOrderLineItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSalesOrderLineItemCommandValidator"/> class
        /// </summary>
        public DeleteSalesOrderLineItemCommandValidator()
        {
            // Id is required
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sales Order Line Item ID is required");
        }
    }
}