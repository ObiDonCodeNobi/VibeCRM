using FluentValidation;

namespace VibeCRM.Application.Features.Call.Commands.DeleteCall
{
    /// <summary>
    /// Validator for the DeleteCallCommand.
    /// Defines validation rules for call deletion commands.
    /// </summary>
    public class DeleteCallCommandValidator : AbstractValidator<DeleteCallCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCallCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeleteCallCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Call ID is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}