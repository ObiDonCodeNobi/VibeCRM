using FluentValidation;

namespace VibeCRM.Application.Features.Activity.Queries.GetAllActivities
{
    /// <summary>
    /// Validator for the GetAllActivitiesQuery.
    /// Validates pagination parameters to ensure they are within acceptable ranges.
    /// </summary>
    public class GetAllActivitiesQueryValidator : AbstractValidator<GetAllActivitiesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllActivitiesQueryValidator class.
        /// Sets up validation rules for the GetAllActivitiesQuery properties.
        /// </summary>
        public GetAllActivitiesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}