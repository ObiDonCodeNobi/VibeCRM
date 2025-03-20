using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByOrderDateRange
{
    /// <summary>
    /// Validator for the GetSalesOrderByOrderDateRangeQuery class
    /// </summary>
    public class GetSalesOrderByOrderDateRangeQueryValidator : AbstractValidator<GetSalesOrderByOrderDateRangeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByOrderDateRangeQueryValidator"/> class.
        /// </summary>
        public GetSalesOrderByOrderDateRangeQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to start date");

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Start date cannot be in the future");

            RuleFor(x => x.EndDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("End date cannot be in the future");
        }
    }
}