using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetAllActivityStatuses
{
    /// <summary>
    /// Validator for the GetAllActivityStatusesQuery class.
    /// This is a parameter-less query, so the validator has no specific rules.
    /// </summary>
    public class GetAllActivityStatusesQueryValidator : AbstractValidator<GetAllActivityStatusesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllActivityStatusesQueryValidator"/> class.
        /// Since this is a parameter-less query, there are no specific validation rules.
        /// </summary>
        public GetAllActivityStatusesQueryValidator()
        {
            // No specific validation rules for this parameter-less query
        }
    }
}