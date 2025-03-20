using FluentValidation;

namespace VibeCRM.Application.Features.Activity.Queries.GetActivityById
{
    /// <summary>
    /// Validator for the GetActivityByIdQuery.
    /// Implements validation rules using FluentValidation to ensure data integrity
    /// when retrieving a specific activity by ID.
    /// </summary>
    public class GetActivityByIdQueryValidator : AbstractValidator<GetActivityByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityByIdQueryValidator"/> class.
        /// Sets up all validation rules for retrieving an activity by ID.
        /// </summary>
        public GetActivityByIdQueryValidator()
        {
            // Activity ID validation
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Activity ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Activity ID must be a valid ID.");
        }
    }
}