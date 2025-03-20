using FluentValidation;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusById
{
    /// <summary>
    /// Validator for the GetPersonStatusByIdQuery.
    /// Defines validation rules for retrieving person status by ID queries.
    /// </summary>
    public class GetPersonStatusByIdQueryValidator : AbstractValidator<GetPersonStatusByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonStatusByIdQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetPersonStatusByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Person status ID is required.");
        }
    }
}