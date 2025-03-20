using FluentValidation;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetPaymentMethodByOrdinalPositionQuery
    /// </summary>
    public class GetPaymentMethodByOrdinalPositionQueryValidator : AbstractValidator<GetPaymentMethodByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetPaymentMethodByOrdinalPositionQueryValidator class
        /// </summary>
        public GetPaymentMethodByOrdinalPositionQueryValidator()
        {
            // No specific validation rules needed for this query as it doesn't have any parameters
        }
    }
}