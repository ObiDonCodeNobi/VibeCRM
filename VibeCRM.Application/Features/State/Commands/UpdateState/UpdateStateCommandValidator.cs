using FluentValidation;

namespace VibeCRM.Application.Features.State.Commands.UpdateState
{
    /// <summary>
    /// Validator for the UpdateStateCommand class.
    /// Defines validation rules for updating an existing state.
    /// </summary>
    public class UpdateStateCommandValidator : AbstractValidator<UpdateStateCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStateCommandValidator"/> class.
        /// Configures validation rules for the UpdateStateCommand properties.
        /// </summary>
        public UpdateStateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("State ID is required.");

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