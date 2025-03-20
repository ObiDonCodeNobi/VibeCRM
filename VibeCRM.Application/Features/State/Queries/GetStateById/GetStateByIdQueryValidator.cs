using FluentValidation;

namespace VibeCRM.Application.Features.State.Queries.GetStateById
{
    /// <summary>
    /// Validator for the GetStateByIdQuery.
    /// Defines validation rules for retrieving a state by ID.
    /// </summary>
    public class GetStateByIdQueryValidator : AbstractValidator<GetStateByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStateByIdQueryValidator"/> class.
        /// Configures validation rules for the GetStateByIdQuery properties.
        /// </summary>
        public GetStateByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("State ID is required.");
        }
    }
}