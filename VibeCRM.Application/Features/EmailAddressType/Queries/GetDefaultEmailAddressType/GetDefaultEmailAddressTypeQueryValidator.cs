using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetDefaultEmailAddressType
{
    /// <summary>
    /// Validator for the GetDefaultEmailAddressTypeQuery.
    /// </summary>
    public class GetDefaultEmailAddressTypeQueryValidator : AbstractValidator<GetDefaultEmailAddressTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultEmailAddressTypeQueryValidator"/> class.
        /// </summary>
        public GetDefaultEmailAddressTypeQueryValidator()
        {
            // No validation rules needed as the query has no parameters
        }
    }
}