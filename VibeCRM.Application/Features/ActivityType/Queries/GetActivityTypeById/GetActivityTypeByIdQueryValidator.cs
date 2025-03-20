using FluentValidation;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeById
{
    /// <summary>
    /// Validator for the GetActivityTypeByIdQuery class.
    /// Defines validation rules for retrieving an activity type by ID.
    /// </summary>
    public class GetActivityTypeByIdQueryValidator : AbstractValidator<GetActivityTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityTypeByIdQueryValidator"/> class.
        /// Configures validation rules for GetActivityTypeByIdQuery properties.
        /// </summary>
        public GetActivityTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Activity type ID is required.");
        }
    }
}