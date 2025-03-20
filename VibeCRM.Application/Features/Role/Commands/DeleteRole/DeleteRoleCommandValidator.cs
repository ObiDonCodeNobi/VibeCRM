using FluentValidation;
using VibeCRM.Application.Features.Role.Commands.DeleteRole;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the DeleteRoleCommand
    /// </summary>
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoleCommandValidator"/> class
        /// </summary>
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required");
        }
    }
}