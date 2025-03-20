using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Commands.DeletePersonType
{
    /// <summary>
    /// Validator for the DeletePersonTypeCommand.
    /// Ensures that the person type ID is valid and exists before processing the command.
    /// </summary>
    public class DeletePersonTypeCommandValidator : AbstractValidator<DeletePersonTypeCommand>
    {
        private readonly IPersonTypeRepository _personTypeRepository;

        /// <summary>
        /// Initializes a new instance of the DeletePersonTypeCommandValidator class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations, used for validation checks.</param>
        public DeletePersonTypeCommandValidator(IPersonTypeRepository personTypeRepository)
        {
            _personTypeRepository = personTypeRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Person type ID is required.")
                .MustAsync(ExistAsync).WithMessage("Person type with the specified ID does not exist.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");
        }

        /// <summary>
        /// Validates that a person type with the specified ID exists in the repository.
        /// </summary>
        /// <param name="id">The ID of the person type to check.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if a person type with the specified ID exists; otherwise, false.</returns>
        private async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _personTypeRepository.ExistsAsync(id, cancellationToken);
        }
    }
}