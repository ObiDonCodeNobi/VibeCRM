using FluentValidation;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetAllPaymentMethods
{
    /// <summary>
    /// Validator for the GetAllPaymentMethodsQuery
    /// </summary>
    public class GetAllPaymentMethodsQueryValidator : AbstractValidator<GetAllPaymentMethodsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllPaymentMethodsQueryValidator class
        /// </summary>
        public GetAllPaymentMethodsQueryValidator()
        {
            // No specific validation rules needed for this query as it doesn't have any parameters
        }
    }
}