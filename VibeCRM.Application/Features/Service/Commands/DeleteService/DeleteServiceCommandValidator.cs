using FluentValidation;

namespace VibeCRM.Application.Features.Service.Commands.DeleteService
{
    /// <summary>
    /// Validator for the DeleteServiceCommand.
    /// Provides validation rules for service deletion operations.
    /// </summary>
    public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteServiceCommandValidator"/> class
        /// and defines the validation rules.
        /// </summary>
        public DeleteServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Service ID is required");

            RuleFor(x => x.ModifiedBy)
                .NotEqual(Guid.Empty).WithMessage("ModifiedBy is required");
        }
    }
}