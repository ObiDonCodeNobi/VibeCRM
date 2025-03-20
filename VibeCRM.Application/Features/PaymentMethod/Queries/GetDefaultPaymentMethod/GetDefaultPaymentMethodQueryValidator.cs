using FluentValidation;
using VibeCRM.Application.Features.PaymentMethod.Queries.GetDefaultPaymentMethod;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetDefaultPaymentMethod
{
    /// <summary>
    /// Validator for the GetDefaultPaymentMethodQuery
    /// </summary>
    public class GetDefaultPaymentMethodQueryValidator : AbstractValidator<GetDefaultPaymentMethodQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetDefaultPaymentMethodQueryValidator class
        /// </summary>
        public GetDefaultPaymentMethodQueryValidator()
        {
            // No specific validation rules needed for this query as it doesn't have any parameters
        }
    }
}
