using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusByStatus
{
    /// <summary>
    /// Validator for the GetSalesOrderStatusByStatusQuery class.
    /// Defines validation rules for retrieving sales order statuses by status name.
    /// </summary>
    public class GetSalesOrderStatusByStatusQueryValidator : AbstractValidator<GetSalesOrderStatusByStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderStatusByStatusQueryValidator"/> class.
        /// Configures validation rules for GetSalesOrderStatusByStatusQuery properties.
        /// </summary>
        public GetSalesOrderStatusByStatusQueryValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status name is required.")
                .MaximumLength(50)
                .WithMessage("Status name cannot exceed 50 characters.");
        }
    }
}