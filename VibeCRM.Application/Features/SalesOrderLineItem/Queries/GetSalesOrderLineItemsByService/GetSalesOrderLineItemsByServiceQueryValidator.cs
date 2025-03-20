using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsByService
{
    /// <summary>
    /// Validator for the GetSalesOrderLineItemsByServiceQuery
    /// </summary>
    public class GetSalesOrderLineItemsByServiceQueryValidator : AbstractValidator<GetSalesOrderLineItemsByServiceQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderLineItemsByServiceQueryValidator"/> class
        /// </summary>
        public GetSalesOrderLineItemsByServiceQueryValidator()
        {
            // ServiceId is required
            RuleFor(x => x.ServiceId)
                .NotEmpty()
                .WithMessage("Service ID is required");
        }
    }
}