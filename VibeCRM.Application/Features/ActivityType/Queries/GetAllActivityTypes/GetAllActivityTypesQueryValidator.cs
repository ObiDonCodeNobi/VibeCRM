using FluentValidation;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetAllActivityTypes
{
    /// <summary>
    /// Validator for the GetAllActivityTypesQuery class.
    /// Defines validation rules for retrieving all activity types.
    /// </summary>
    public class GetAllActivityTypesQueryValidator : AbstractValidator<GetAllActivityTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllActivityTypesQueryValidator"/> class.
        /// Configures validation rules for GetAllActivityTypesQuery properties.
        /// </summary>
        public GetAllActivityTypesQueryValidator()
        {
            // No specific validation rules needed for this query as it has no parameters
        }
    }
}