using FluentValidation;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetShipMethodByMethod
{
    /// <summary>
    /// Validator for the GetShipMethodByMethodQuery.
    /// Defines validation rules for retrieving shipping methods by method name.
    /// </summary>
    public class GetShipMethodByMethodQueryValidator : AbstractValidator<GetShipMethodByMethodQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetShipMethodByMethodQueryValidator"/> class.
        /// Configures validation rules for the GetShipMethodByMethodQuery properties.
        /// </summary>
        public GetShipMethodByMethodQueryValidator()
        {
            RuleFor(x => x.Method)
                .NotEmpty()
                .WithMessage("Method name to search for is required.")
                .MaximumLength(50)
                .WithMessage("Method name cannot exceed 50 characters.");
        }
    }
}