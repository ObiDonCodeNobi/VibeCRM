using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypesByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetEmailAddressTypesByOrdinalPositionQuery.
    /// </summary>
    public class GetEmailAddressTypesByOrdinalPositionQueryValidator : AbstractValidator<GetEmailAddressTypesByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEmailAddressTypesByOrdinalPositionQueryValidator"/> class.
        /// </summary>
        public GetEmailAddressTypesByOrdinalPositionQueryValidator()
        {
            // No validation rules needed as the query has no parameters
        }
    }
}