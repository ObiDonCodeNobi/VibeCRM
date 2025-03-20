using FluentValidation;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetAllPhoneTypes
{
    /// <summary>
    /// Validator for the GetAllPhoneTypesQuery class.
    /// Since this query has no parameters, the validator is empty.
    /// </summary>
    public class GetAllPhoneTypesQueryValidator : AbstractValidator<GetAllPhoneTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPhoneTypesQueryValidator"/> class.
        /// </summary>
        public GetAllPhoneTypesQueryValidator()
        {
            // No validation rules needed since the query has no parameters
        }
    }
}
