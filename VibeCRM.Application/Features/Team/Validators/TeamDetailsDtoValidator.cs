using FluentValidation;
using VibeCRM.Shared.DTOs.Team;

namespace VibeCRM.Application.Features.Team.Validators
{
    /// <summary>
    /// Validator for the TeamDetailsDto
    /// </summary>
    public class TeamDetailsDtoValidator : AbstractValidator<TeamDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamDetailsDtoValidator"/> class
        /// </summary>
        public TeamDetailsDtoValidator()
        {
            // Include all rules from the base validator
            Include(new TeamDtoValidator());

            // CreatedBy is required
            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created By is required");

            // CreatedDate is required
            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Created Date is required");

            // ModifiedBy is required
            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified By is required");

            // ModifiedDate is required
            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modified Date is required");

            // MemberCount must be non-negative
            RuleFor(x => x.MemberCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Member Count must be a non-negative number");
        }
    }
}