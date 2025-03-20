using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusByStatus
{
    /// <summary>
    /// Validator for the GetActivityStatusByStatusQuery class.
    /// Defines validation rules for retrieving activity statuses by status name.
    /// </summary>
    public class GetActivityStatusByStatusQueryValidator : AbstractValidator<GetActivityStatusByStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityStatusByStatusQueryValidator"/> class.
        /// Configures validation rules for GetActivityStatusByStatusQuery properties.
        /// </summary>
        public GetActivityStatusByStatusQueryValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name cannot exceed 100 characters.");
        }
    }
}