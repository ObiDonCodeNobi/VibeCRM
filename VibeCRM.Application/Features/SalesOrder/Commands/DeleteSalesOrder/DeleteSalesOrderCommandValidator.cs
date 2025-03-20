using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Commands.DeleteSalesOrder
{
    /// <summary>
    /// Validator for the DeleteSalesOrderCommand class
    /// </summary>
    public class DeleteSalesOrderCommandValidator : AbstractValidator<DeleteSalesOrderCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSalesOrderCommandValidator"/> class.
        /// </summary>
        public DeleteSalesOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sales order ID is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required");
        }
    }
}