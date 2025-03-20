using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemById
{
    /// <summary>
    /// Validator for the GetSalesOrderLineItemByIdQuery
    /// </summary>
    public class GetSalesOrderLineItemByIdQueryValidator : AbstractValidator<GetSalesOrderLineItemByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderLineItemByIdQueryValidator"/> class
        /// </summary>
        public GetSalesOrderLineItemByIdQueryValidator()
        {
            // Id is required
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sales Order Line Item ID is required");
        }
    }
}