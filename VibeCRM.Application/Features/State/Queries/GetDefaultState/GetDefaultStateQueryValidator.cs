using FluentValidation;

namespace VibeCRM.Application.Features.State.Queries.GetDefaultState
{
    /// <summary>
    /// Validator for the GetDefaultStateQuery.
    /// Defines validation rules for retrieving the default state.
    /// </summary>
    public class GetDefaultStateQueryValidator : AbstractValidator<GetDefaultStateQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultStateQueryValidator"/> class.
        /// Since GetDefaultStateQuery has no properties to validate, this validator has no rules.
        /// </summary>
        public GetDefaultStateQueryValidator()
        {
            // No properties to validate in GetDefaultStateQuery
        }
    }
}