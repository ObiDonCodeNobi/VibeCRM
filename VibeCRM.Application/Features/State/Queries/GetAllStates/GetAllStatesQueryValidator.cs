using FluentValidation;

namespace VibeCRM.Application.Features.State.Queries.GetAllStates
{
    /// <summary>
    /// Validator for the GetAllStatesQuery.
    /// Defines validation rules for retrieving all states.
    /// </summary>
    public class GetAllStatesQueryValidator : AbstractValidator<GetAllStatesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllStatesQueryValidator"/> class.
        /// Since GetAllStatesQuery has no properties to validate, this validator has no rules.
        /// </summary>
        public GetAllStatesQueryValidator()
        {
            // No properties to validate in GetAllStatesQuery
        }
    }
}
