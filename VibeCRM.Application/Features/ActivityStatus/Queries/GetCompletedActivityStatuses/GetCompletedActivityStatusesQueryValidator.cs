using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetCompletedActivityStatuses
{
    /// <summary>
    /// Validator for the GetCompletedActivityStatusesQuery class.
    /// This is a parameter-less query, so the validator has no specific rules.
    /// </summary>
    public class GetCompletedActivityStatusesQueryValidator : AbstractValidator<GetCompletedActivityStatusesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCompletedActivityStatusesQueryValidator"/> class.
        /// Since this is a parameter-less query, there are no specific validation rules.
        /// </summary>
        public GetCompletedActivityStatusesQueryValidator()
        {
            // No specific validation rules for this parameter-less query
        }
    }
}