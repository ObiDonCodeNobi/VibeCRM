using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByCompany
{
    /// <summary>
    /// Validator for the GetSalesOrderByCompanyQuery class
    /// </summary>
    public class GetSalesOrderByCompanyQueryValidator : AbstractValidator<GetSalesOrderByCompanyQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByCompanyQueryValidator"/> class.
        /// </summary>
        public GetSalesOrderByCompanyQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("Company ID is required");
        }
    }
}