using FluentValidation;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the RoleDto
    /// </summary>
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDtoValidator"/> class
        /// </summary>
        public RoleDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");
        }
    }
}