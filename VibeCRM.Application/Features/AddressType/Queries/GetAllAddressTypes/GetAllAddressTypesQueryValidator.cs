using FluentValidation;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAllAddressTypes
{
    /// <summary>
    /// Validator for the GetAllAddressTypesQuery class.
    /// Since this query has no parameters, this validator has no specific rules.
    /// It is included for consistency with the validation pattern.
    /// </summary>
    public class GetAllAddressTypesQueryValidator : AbstractValidator<GetAllAddressTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAddressTypesQueryValidator"/> class.
        /// </summary>
        public GetAllAddressTypesQueryValidator()
        {
            // No validation rules needed for this query as it has no parameters
        }
    }
}