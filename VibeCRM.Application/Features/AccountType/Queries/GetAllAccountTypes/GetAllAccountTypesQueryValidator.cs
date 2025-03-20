using FluentValidation;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAllAccountTypes
{
    /// <summary>
    /// Validator for the GetAllAccountTypesQuery class.
    /// This is a parameter-less query, so the validator has no specific rules.
    /// </summary>
    public class GetAllAccountTypesQueryValidator : AbstractValidator<GetAllAccountTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAccountTypesQueryValidator"/> class.
        /// Since this is a parameter-less query, there are no specific validation rules.
        /// </summary>
        public GetAllAccountTypesQueryValidator()
        {
            // No specific validation rules for this parameter-less query
        }
    }
}