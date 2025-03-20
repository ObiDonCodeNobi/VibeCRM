using FluentValidation;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetPhoneTypeByOrdinalPositionQuery class.
    /// Defines validation rules for retrieving a phone type by ordinal position.
    /// </summary>
    public class GetPhoneTypeByOrdinalPositionQueryValidator : AbstractValidator<GetPhoneTypeByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhoneTypeByOrdinalPositionQueryValidator"/> class.
        /// Configures validation rules for GetPhoneTypeByOrdinalPositionQuery properties.
        /// </summary>
        public GetPhoneTypeByOrdinalPositionQueryValidator()
        {
            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}