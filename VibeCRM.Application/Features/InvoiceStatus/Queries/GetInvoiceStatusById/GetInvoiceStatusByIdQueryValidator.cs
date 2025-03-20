using FluentValidation;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusById
{
    /// <summary>
    /// Validator for the GetInvoiceStatusByIdQuery
    /// </summary>
    public class GetInvoiceStatusByIdQueryValidator : AbstractValidator<GetInvoiceStatusByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetInvoiceStatusByIdQueryValidator class
        /// </summary>
        public GetInvoiceStatusByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
