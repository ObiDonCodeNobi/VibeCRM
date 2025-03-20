using FluentValidation;

namespace VibeCRM.Application.Features.Phone.Queries.GetAllPhones
{
    /// <summary>
    /// Validator for the GetAllPhonesQuery
    /// </summary>
    public class GetAllPhonesQueryValidator : AbstractValidator<GetAllPhonesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPhonesQueryValidator"/> class
        /// </summary>
        /// <remarks>
        /// This validator doesn't have any specific rules since the GetAllPhonesQuery doesn't have any properties to validate.
        /// It's included for consistency with the validation pattern used throughout the application.
        /// </remarks>
        public GetAllPhonesQueryValidator()
        {
            // No validation rules needed as the query has no properties
        }
    }
}