using FluentValidation;

namespace VibeCRM.Application.Features.State.Queries.GetStatesByAbbreviation
{
    /// <summary>
    /// Validator for the GetStatesByAbbreviationQuery.
    /// Defines validation rules for retrieving states by abbreviation.
    /// </summary>
    public class GetStatesByAbbreviationQueryValidator : AbstractValidator<GetStatesByAbbreviationQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStatesByAbbreviationQueryValidator"/> class.
        /// Configures validation rules for the GetStatesByAbbreviationQuery properties.
        /// </summary>
        public GetStatesByAbbreviationQueryValidator()
        {
            RuleFor(x => x.Abbreviation)
                .NotEmpty()
                .WithMessage("State abbreviation is required for search.")
                .MaximumLength(10)
                .WithMessage("State abbreviation cannot exceed 10 characters.");
        }
    }
}