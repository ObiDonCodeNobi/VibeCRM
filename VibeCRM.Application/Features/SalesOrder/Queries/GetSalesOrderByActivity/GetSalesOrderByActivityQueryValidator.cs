using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByActivity
{
    /// <summary>
    /// Validator for the GetSalesOrderByActivityQuery class
    /// </summary>
    public class GetSalesOrderByActivityQueryValidator : AbstractValidator<GetSalesOrderByActivityQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByActivityQueryValidator"/> class.
        /// </summary>
        public GetSalesOrderByActivityQueryValidator()
        {
            RuleFor(x => x.ActivityId)
                .NotEmpty().WithMessage("Activity ID is required");
        }
    }
}