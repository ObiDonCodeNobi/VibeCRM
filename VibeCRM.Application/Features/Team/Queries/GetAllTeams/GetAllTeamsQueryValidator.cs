using FluentValidation;

namespace VibeCRM.Application.Features.Team.Queries.GetAllTeams
{
    /// <summary>
    /// Validator for the GetAllTeamsQuery
    /// </summary>
    public class GetAllTeamsQueryValidator : AbstractValidator<GetAllTeamsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllTeamsQueryValidator"/> class
        /// </summary>
        public GetAllTeamsQueryValidator()
        {
            // No specific validation rules needed for this query
            // as it doesn't have any properties to validate
        }
    }
}