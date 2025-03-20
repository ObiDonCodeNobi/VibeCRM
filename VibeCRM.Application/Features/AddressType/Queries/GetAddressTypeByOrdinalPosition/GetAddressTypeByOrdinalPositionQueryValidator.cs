using FluentValidation;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetAddressTypeByOrdinalPositionQuery class.
    /// Defines validation rules for retrieving an address type by its ordinal position.
    /// </summary>
    public class GetAddressTypeByOrdinalPositionQueryValidator : AbstractValidator<GetAddressTypeByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAddressTypeByOrdinalPositionQueryValidator"/> class.
        /// Configures validation rules for GetAddressTypeByOrdinalPositionQuery properties.
        /// </summary>
        public GetAddressTypeByOrdinalPositionQueryValidator()
        {
            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}