using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetAllSalesOrders
{
    /// <summary>
    /// Validator for the GetAllSalesOrdersQuery class
    /// </summary>
    public class GetAllSalesOrdersQueryValidator : AbstractValidator<GetAllSalesOrdersQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllSalesOrdersQueryValidator"/> class.
        /// </summary>
        public GetAllSalesOrdersQueryValidator()
        {
            // No specific validation rules required for this query
            // The IncludeInactive property is a boolean with a default value
        }
    }
}