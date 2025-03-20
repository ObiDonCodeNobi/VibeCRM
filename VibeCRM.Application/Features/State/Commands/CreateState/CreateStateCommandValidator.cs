using FluentValidation;

namespace VibeCRM.Application.Features.State.Commands.CreateState
{
    /// <summary>
    /// Validator for the CreateStateCommand class.
    /// Defines validation rules for creating a new state.
    /// </summary>
    public class CreateStateCommandValidator : AbstractValidator<CreateStateCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStateCommandValidator"/> class.
        /// Configures validation rules for the CreateStateCommand properties.
        /// </summary>
        public CreateStateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("State name is required.")
                .MaximumLength(100)
                .WithMessage("State name cannot exceed 100 characters.");

            RuleFor(x => x.Abbreviation)
                .NotEmpty()
                .WithMessage("State abbreviation is required.")
                .MaximumLength(10)
                .WithMessage("State abbreviation cannot exceed 10 characters.");

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required.")
                .MaximumLength(2)
                .WithMessage("Country code should be 2 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}