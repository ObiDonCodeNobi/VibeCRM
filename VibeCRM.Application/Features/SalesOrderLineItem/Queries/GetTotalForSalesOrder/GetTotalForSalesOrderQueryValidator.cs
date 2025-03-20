using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetTotalForSalesOrder
{
    /// <summary>
    /// Validator for the GetTotalForSalesOrderQuery
    /// </summary>
    public class GetTotalForSalesOrderQueryValidator : AbstractValidator<GetTotalForSalesOrderQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTotalForSalesOrderQueryValidator"/> class
        /// </summary>
        public GetTotalForSalesOrderQueryValidator()
        {
            // SalesOrderId is required
            RuleFor(x => x.SalesOrderId)
                .NotEmpty()
                .WithMessage("Sales Order ID is required");
        }
    }
}