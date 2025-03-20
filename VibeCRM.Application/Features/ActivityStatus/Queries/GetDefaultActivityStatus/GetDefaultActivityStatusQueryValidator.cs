using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetDefaultActivityStatus
{
    /// <summary>
    /// Validator for the GetDefaultActivityStatusQuery class.
    /// This is a parameter-less query, so the validator has no specific rules.
    /// </summary>
    public class GetDefaultActivityStatusQueryValidator : AbstractValidator<GetDefaultActivityStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultActivityStatusQueryValidator"/> class.
        /// Since this is a parameter-less query, there are no specific validation rules.
        /// </summary>
        public GetDefaultActivityStatusQueryValidator()
        {
            // No specific validation rules for this parameter-less query
        }
    }
}