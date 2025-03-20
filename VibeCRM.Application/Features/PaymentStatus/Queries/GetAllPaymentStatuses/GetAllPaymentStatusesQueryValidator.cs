using FluentValidation;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetAllPaymentStatuses
{
    /// <summary>
    /// Validator for the GetAllPaymentStatusesQuery.
    /// Defines validation rules for retrieving all payment statuses queries.
    /// </summary>
    public class GetAllPaymentStatusesQueryValidator : AbstractValidator<GetAllPaymentStatusesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPaymentStatusesQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetAllPaymentStatusesQueryValidator()
        {
            // No specific validation rules needed for this query
            // The IncludeInactive property is a boolean with a default value
        }
    }
}