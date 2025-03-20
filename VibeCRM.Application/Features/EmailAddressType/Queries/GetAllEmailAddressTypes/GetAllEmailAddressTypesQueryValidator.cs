using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetAllEmailAddressTypes
{
    /// <summary>
    /// Validator for the GetAllEmailAddressTypesQuery.
    /// </summary>
    public class GetAllEmailAddressTypesQueryValidator : AbstractValidator<GetAllEmailAddressTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllEmailAddressTypesQueryValidator"/> class.
        /// </summary>
        public GetAllEmailAddressTypesQueryValidator()
        {
            // No validation rules needed as the query has no parameters
        }
    }
}