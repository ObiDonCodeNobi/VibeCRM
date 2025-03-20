using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetAllSalesOrderStatuses
{
    /// <summary>
    /// Validator for the GetAllSalesOrderStatusesQuery class.
    /// Defines validation rules for retrieving all sales order statuses.
    /// </summary>
    public class GetAllSalesOrderStatusesQueryValidator : AbstractValidator<GetAllSalesOrderStatusesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllSalesOrderStatusesQueryValidator"/> class.
        /// No specific validation rules are needed for this query as it doesn't have any parameters.
        /// </summary>
        public GetAllSalesOrderStatusesQueryValidator()
        {
            // No specific validation rules are needed for this query
            // as it doesn't have any parameters
        }
    }
}
