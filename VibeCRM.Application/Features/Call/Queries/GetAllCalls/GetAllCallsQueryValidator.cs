using FluentValidation;

namespace VibeCRM.Application.Features.Call.Queries.GetAllCalls
{
    /// <summary>
    /// Validator for the GetAllCallsQuery to ensure the query contains valid pagination parameters.
    /// </summary>
    public class GetAllCallsQueryValidator : AbstractValidator<GetAllCallsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllCallsQueryValidator class with validation rules.
        /// </summary>
        public GetAllCallsQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}