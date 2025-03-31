using FluentValidation;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the RoleListDto
    /// </summary>
    public class RoleListDtoValidator : AbstractValidator<RoleListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleListDtoValidator"/> class
        /// </summary>
        public RoleListDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required");
        }
    }
}