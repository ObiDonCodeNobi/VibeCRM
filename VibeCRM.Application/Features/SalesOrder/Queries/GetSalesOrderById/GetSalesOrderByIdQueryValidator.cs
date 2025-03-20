using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderById
{
    /// <summary>
    /// Validator for the GetSalesOrderByIdQuery class
    /// </summary>
    public class GetSalesOrderByIdQueryValidator : AbstractValidator<GetSalesOrderByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByIdQueryValidator"/> class.
        /// </summary>
        public GetSalesOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sales order ID is required");
        }
    }
}