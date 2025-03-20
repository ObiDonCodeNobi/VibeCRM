using FluentValidation;
using VibeCRM.Application.Features.Role.Commands.UpdateRole;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the UpdateRoleCommand
    /// </summary>
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleCommandValidator"/> class
        /// </summary>
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required");
        }
    }
}