using FluentValidation;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetAccountTypeByOrdinalPositionQuery class.
    /// This is a parameter-less query, so the validator has no specific rules.
    /// </summary>
    public class GetAccountTypeByOrdinalPositionQueryValidator : AbstractValidator<GetAccountTypeByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountTypeByOrdinalPositionQueryValidator"/> class.
        /// Since this is a parameter-less query, there are no specific validation rules.
        /// </summary>
        public GetAccountTypeByOrdinalPositionQueryValidator()
        {
            // No specific validation rules for this parameter-less query
        }
    }
}