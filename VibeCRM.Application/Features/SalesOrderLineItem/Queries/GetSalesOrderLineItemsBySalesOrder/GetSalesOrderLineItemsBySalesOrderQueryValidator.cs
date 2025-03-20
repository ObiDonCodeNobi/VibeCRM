using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsBySalesOrder
{
    /// <summary>
    /// Validator for the GetSalesOrderLineItemsBySalesOrderQuery
    /// </summary>
    public class GetSalesOrderLineItemsBySalesOrderQueryValidator : AbstractValidator<GetSalesOrderLineItemsBySalesOrderQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderLineItemsBySalesOrderQueryValidator"/> class
        /// </summary>
        public GetSalesOrderLineItemsBySalesOrderQueryValidator()
        {
            // SalesOrderId is required
            RuleFor(x => x.SalesOrderId)
                .NotEmpty()
                .WithMessage("Sales Order ID is required");
        }
    }
}