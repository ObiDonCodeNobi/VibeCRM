using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Commands.DeleteCallType
{
    /// <summary>
    /// Validator for the DeleteCallTypeCommand.
    /// Defines validation rules for deleting an existing call type.
    /// </summary>
    public class DeleteCallTypeCommandValidator : AbstractValidator<DeleteCallTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCallTypeCommandValidator"/> class.
        /// Sets up validation rules for the DeleteCallTypeCommand properties.
        /// </summary>
        public DeleteCallTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}