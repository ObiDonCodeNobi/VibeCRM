using FluentValidation;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetDefaultPhoneType
{
    /// <summary>
    /// Validator for the GetDefaultPhoneTypeQuery class.
    /// Since this query has no parameters, the validator is empty.
    /// </summary>
    public class GetDefaultPhoneTypeQueryValidator : AbstractValidator<GetDefaultPhoneTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultPhoneTypeQueryValidator"/> class.
        /// </summary>
        public GetDefaultPhoneTypeQueryValidator()
        {
            // No validation rules needed since the query has no parameters
        }
    }
}
