using FluentValidation;

namespace VibeCRM.Application.Features.ActivityDefinition.Queries.GetAllActivityDefinitions
{
    /// <summary>
    /// Validator for the GetAllActivityDefinitionsQuery
    /// </summary>
    public class GetAllActivityDefinitionsQueryValidator : AbstractValidator<GetAllActivityDefinitionsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllActivityDefinitionsQueryValidator class
        /// </summary>
        public GetAllActivityDefinitionsQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}