using FluentValidation;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetInvoiceStatusByOrdinalPositionQuery
    /// </summary>
    public class GetInvoiceStatusByOrdinalPositionQueryValidator : AbstractValidator<GetInvoiceStatusByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetInvoiceStatusByOrdinalPositionQueryValidator class
        /// </summary>
        public GetInvoiceStatusByOrdinalPositionQueryValidator()
        {
            // No specific validation rules needed for this query
            // as it doesn't have any required parameters
        }
    }
}
