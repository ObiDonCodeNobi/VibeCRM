using FluentValidation;

namespace VibeCRM.Application.Features.ShipMethod.Commands.DeleteShipMethod
{
    /// <summary>
    /// Validator for the DeleteShipMethodCommand.
    /// Defines validation rules for deleting an existing shipping method.
    /// </summary>
    public class DeleteShipMethodCommandValidator : AbstractValidator<DeleteShipMethodCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteShipMethodCommandValidator"/> class.
        /// Configures validation rules for the DeleteShipMethodCommand properties.
        /// </summary>
        public DeleteShipMethodCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Shipping method ID is required.");
        }
    }
}