using FluentValidation;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusByStatus
{
    /// <summary>
    /// Validator for the GetInvoiceStatusByStatusQuery
    /// </summary>
    public class GetInvoiceStatusByStatusQueryValidator : AbstractValidator<GetInvoiceStatusByStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetInvoiceStatusByStatusQueryValidator class
        /// </summary>
        public GetInvoiceStatusByStatusQueryValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .MaximumLength(100).WithMessage("Status cannot exceed 100 characters");
        }
    }
}
