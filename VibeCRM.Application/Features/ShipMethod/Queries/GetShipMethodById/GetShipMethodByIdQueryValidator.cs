using System;
using FluentValidation;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetShipMethodById
{
    /// <summary>
    /// Validator for the GetShipMethodByIdQuery.
    /// Defines validation rules for retrieving a shipping method by ID.
    /// </summary>
    public class GetShipMethodByIdQueryValidator : AbstractValidator<GetShipMethodByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetShipMethodByIdQueryValidator"/> class.
        /// Configures validation rules for the GetShipMethodByIdQuery properties.
        /// </summary>
        public GetShipMethodByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Shipping method ID is required.");
        }
    }
}
