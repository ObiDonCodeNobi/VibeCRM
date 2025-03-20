using FluentValidation;

namespace VibeCRM.Application.Features.ServiceType.Commands.DeleteServiceType
{
    /// <summary>
    /// Validator for the DeleteServiceTypeCommand.
    /// Defines validation rules for deleting an existing service type.
    /// </summary>
    public class DeleteServiceTypeCommandValidator : AbstractValidator<DeleteServiceTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteServiceTypeCommandValidator"/> class.
        /// Configures validation rules for the DeleteServiceTypeCommand properties.
        /// </summary>
        public DeleteServiceTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Service type ID is required.");
        }
    }
}