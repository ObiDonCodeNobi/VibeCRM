using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetSalesOrderStatusByOrdinalPositionQuery class.
    /// Defines validation rules for retrieving sales order statuses by ordinal position.
    /// </summary>
    public class GetSalesOrderStatusByOrdinalPositionQueryValidator : AbstractValidator<GetSalesOrderStatusByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderStatusByOrdinalPositionQueryValidator"/> class.
        /// Configures validation rules for GetSalesOrderStatusByOrdinalPositionQuery properties.
        /// </summary>
        public GetSalesOrderStatusByOrdinalPositionQueryValidator()
        {
            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}