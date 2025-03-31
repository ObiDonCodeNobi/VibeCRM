using FluentValidation;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the RoleDetailsDto
    /// </summary>
    public class RoleDetailsDtoValidator : AbstractValidator<RoleDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDetailsDtoValidator"/> class
        /// </summary>
        public RoleDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required");
        }
    }
}