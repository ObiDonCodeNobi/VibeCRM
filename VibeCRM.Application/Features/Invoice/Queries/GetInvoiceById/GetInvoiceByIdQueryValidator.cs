using FluentValidation;

namespace VibeCRM.Application.Features.Invoice.Queries.GetInvoiceById
{
    /// <summary>
    /// Validator for the GetInvoiceByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetInvoiceByIdQueryValidator : AbstractValidator<GetInvoiceByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetInvoiceByIdQueryValidator class with validation rules.
        /// </summary>
        public GetInvoiceByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}