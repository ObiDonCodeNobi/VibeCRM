using FluentValidation;

namespace VibeCRM.Application.Features.Person.Queries.GetAllPersons
{
    /// <summary>
    /// Validator for the GetAllPersonsQuery class.
    /// Defines validation rules for retrieving lists of persons.
    /// </summary>
    public class GetAllPersonsQueryValidator : AbstractValidator<GetAllPersonsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPersonsQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetAllPersonsQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(q => q.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100 for performance reasons.");

            RuleFor(q => q.SearchTerm)
                .MaximumLength(200).WithMessage("Search term must not exceed 200 characters.")
                .When(q => !string.IsNullOrEmpty(q.SearchTerm));
        }
    }
}