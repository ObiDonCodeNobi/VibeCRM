using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetAllSalesOrderLineItems
{
    /// <summary>
    /// Validator for the GetAllSalesOrderLineItemsQuery
    /// </summary>
    public class GetAllSalesOrderLineItemsQueryValidator : AbstractValidator<GetAllSalesOrderLineItemsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllSalesOrderLineItemsQueryValidator"/> class
        /// </summary>
        public GetAllSalesOrderLineItemsQueryValidator()
        {
            // No specific validation rules required for this query
            // as it doesn't have any parameters
        }
    }
}