using FluentValidation;

namespace VibeCRM.Application.Features.State.Queries.GetStatesByName
{
    /// <summary>
    /// Validator for the GetStatesByNameQuery.
    /// Defines validation rules for retrieving states by name.
    /// </summary>
    public class GetStatesByNameQueryValidator : AbstractValidator<GetStatesByNameQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStatesByNameQueryValidator"/> class.
        /// Configures validation rules for the GetStatesByNameQuery properties.
        /// </summary>
        public GetStatesByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("State name is required for search.")
                .MaximumLength(100)
                .WithMessage("State name cannot exceed 100 characters.");
        }
    }
}