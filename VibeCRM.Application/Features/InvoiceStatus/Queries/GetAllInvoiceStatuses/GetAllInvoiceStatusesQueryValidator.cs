using FluentValidation;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetAllInvoiceStatuses
{
    /// <summary>
    /// Validator for the GetAllInvoiceStatusesQuery
    /// </summary>
    public class GetAllInvoiceStatusesQueryValidator : AbstractValidator<GetAllInvoiceStatusesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllInvoiceStatusesQueryValidator class
        /// </summary>
        public GetAllInvoiceStatusesQueryValidator()
        {
            // No specific validation rules needed for this query
            // as it doesn't have any required parameters
        }
    }
}