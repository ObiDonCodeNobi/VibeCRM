using FluentValidation;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeByType
{
    /// <summary>
    /// Validator for the GetActivityTypeByTypeQuery class.
    /// Defines validation rules for retrieving an activity type by type name.
    /// </summary>
    public class GetActivityTypeByTypeQueryValidator : AbstractValidator<GetActivityTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityTypeByTypeQueryValidator"/> class.
        /// Configures validation rules for GetActivityTypeByTypeQuery properties.
        /// </summary>
        public GetActivityTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type name is required.")
                .MaximumLength(100).WithMessage("Type name cannot exceed 100 characters.");
        }
    }
}