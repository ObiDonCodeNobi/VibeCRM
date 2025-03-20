using FluentValidation;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetDefaultInvoiceStatus
{
    /// <summary>
    /// Validator for the GetDefaultInvoiceStatusQuery
    /// </summary>
    public class GetDefaultInvoiceStatusQueryValidator : AbstractValidator<GetDefaultInvoiceStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetDefaultInvoiceStatusQueryValidator class
        /// </summary>
        public GetDefaultInvoiceStatusQueryValidator()
        {
            // No specific validation rules needed for this query
            // as it doesn't have any required parameters
        }
    }
}
