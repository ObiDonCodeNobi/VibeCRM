using FluentValidation;

namespace VibeCRM.Application.Features.Person.Queries.GetPersonById
{
    /// <summary>
    /// Validator for the GetPersonByIdQuery class.
    /// Defines validation rules for retrieving a person by ID.
    /// </summary>
    public class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonByIdQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetPersonByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Person ID is required for retrieval.");
        }
    }
}