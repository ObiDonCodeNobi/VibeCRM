using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByNumber
{
    /// <summary>
    /// Validator for the GetSalesOrderByNumberQuery class
    /// </summary>
    public class GetSalesOrderByNumberQueryValidator : AbstractValidator<GetSalesOrderByNumberQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByNumberQueryValidator"/> class.
        /// </summary>
        public GetSalesOrderByNumberQueryValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Sales order number is required")
                .MaximumLength(50).WithMessage("Sales order number cannot exceed 50 characters");
        }
    }
}