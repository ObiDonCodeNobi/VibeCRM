using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsByProduct
{
    /// <summary>
    /// Validator for the GetSalesOrderLineItemsByProductQuery
    /// </summary>
    public class GetSalesOrderLineItemsByProductQueryValidator : AbstractValidator<GetSalesOrderLineItemsByProductQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderLineItemsByProductQueryValidator"/> class
        /// </summary>
        public GetSalesOrderLineItemsByProductQueryValidator()
        {
            // ProductId is required
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}