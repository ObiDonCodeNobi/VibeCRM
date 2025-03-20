using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusById
{
    /// <summary>
    /// Validator for the GetActivityStatusByIdQuery class.
    /// Defines validation rules for retrieving an activity status by ID.
    /// </summary>
    public class GetActivityStatusByIdQueryValidator : AbstractValidator<GetActivityStatusByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityStatusByIdQueryValidator"/> class.
        /// Configures validation rules for GetActivityStatusByIdQuery properties.
        /// </summary>
        public GetActivityStatusByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Activity status ID is required.");
        }
    }
}