using FluentValidation;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetDefaultActivityType
{
    /// <summary>
    /// Validator for the GetDefaultActivityTypeQuery class.
    /// Defines validation rules for retrieving the default activity type.
    /// </summary>
    public class GetDefaultActivityTypeQueryValidator : AbstractValidator<GetDefaultActivityTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultActivityTypeQueryValidator"/> class.
        /// Configures validation rules for GetDefaultActivityTypeQuery properties.
        /// </summary>
        public GetDefaultActivityTypeQueryValidator()
        {
            // No specific validation rules needed for this query as it has no parameters
        }
    }
}