using FluentValidation;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetAllShipMethods
{
    /// <summary>
    /// Validator for the GetAllShipMethodsQuery.
    /// Defines validation rules for retrieving all shipping methods.
    /// </summary>
    public class GetAllShipMethodsQueryValidator : AbstractValidator<GetAllShipMethodsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllShipMethodsQueryValidator"/> class.
        /// Since GetAllShipMethodsQuery has no properties to validate, this validator has no rules.
        /// </summary>
        public GetAllShipMethodsQueryValidator()
        {
            // No properties to validate in GetAllShipMethodsQuery
        }
    }
}