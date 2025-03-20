using FluentValidation;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetDefaultShipMethod
{
    /// <summary>
    /// Validator for the GetDefaultShipMethodQuery.
    /// Defines validation rules for retrieving the default shipping method.
    /// </summary>
    public class GetDefaultShipMethodQueryValidator : AbstractValidator<GetDefaultShipMethodQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultShipMethodQueryValidator"/> class.
        /// Since GetDefaultShipMethodQuery has no properties to validate, this validator has no rules.
        /// </summary>
        public GetDefaultShipMethodQueryValidator()
        {
            // No properties to validate in GetDefaultShipMethodQuery
        }
    }
}
