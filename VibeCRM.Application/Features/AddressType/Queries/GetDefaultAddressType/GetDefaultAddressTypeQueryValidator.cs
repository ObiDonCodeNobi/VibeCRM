using FluentValidation;

namespace VibeCRM.Application.Features.AddressType.Queries.GetDefaultAddressType
{
    /// <summary>
    /// Validator for the GetDefaultAddressTypeQuery class.
    /// Since this query has no parameters, this validator has no specific rules.
    /// It is included for consistency with the validation pattern.
    /// </summary>
    public class GetDefaultAddressTypeQueryValidator : AbstractValidator<GetDefaultAddressTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultAddressTypeQueryValidator"/> class.
        /// </summary>
        public GetDefaultAddressTypeQueryValidator()
        {
            // No validation rules needed for this query as it has no parameters
        }
    }
}