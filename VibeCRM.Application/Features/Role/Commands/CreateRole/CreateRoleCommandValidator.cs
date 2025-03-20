using FluentValidation;
using VibeCRM.Application.Features.Role.Commands.CreateRole;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the CreateRoleCommand
    /// </summary>
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoleCommandValidator"/> class
        /// </summary>
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required");
        }
    }
}