using FluentValidation;

namespace VibeCRM.Application.Features.ActivityDefinition.Queries.GetActivityDefinitionById
{
    /// <summary>
    /// Validator for the GetActivityDefinitionByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetActivityDefinitionByIdQueryValidator : AbstractValidator<GetActivityDefinitionByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetActivityDefinitionByIdQueryValidator class with validation rules.
        /// </summary>
        public GetActivityDefinitionByIdQueryValidator()
        {
            RuleFor(q => q.ActivityDefinitionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}