using FluentValidation;

namespace VibeCRM.Application.Features.Call.Queries.GetCallById
{
    /// <summary>
    /// Validator for the GetCallByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetCallByIdQueryValidator : AbstractValidator<GetCallByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetCallByIdQueryValidator class with validation rules.
        /// </summary>
        public GetCallByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}