using FluentValidation;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetDefaultPersonStatus
{
    /// <summary>
    /// Validator for the GetDefaultPersonStatusQuery.
    /// Defines validation rules for retrieving the default person status query.
    /// </summary>
    public class GetDefaultPersonStatusQueryValidator : AbstractValidator<GetDefaultPersonStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultPersonStatusQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetDefaultPersonStatusQueryValidator()
        {
            // No specific validation rules needed for this query
            // This query does not have any parameters to validate
        }
    }
}
