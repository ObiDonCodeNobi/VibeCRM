using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Commands.UpdatePersonType
{
    /// <summary>
    /// Validator for the UpdatePersonTypeCommand.
    /// Ensures that all required fields are provided and valid before processing the command.
    /// </summary>
    public class UpdatePersonTypeCommandValidator : AbstractValidator<UpdatePersonTypeCommand>
    {
        private readonly IPersonTypeRepository _personTypeRepository;

        /// <summary>
        /// Initializes a new instance of the UpdatePersonTypeCommandValidator class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations, used for validation checks.</param>
        public UpdatePersonTypeCommandValidator(IPersonTypeRepository personTypeRepository)
        {
            _personTypeRepository = personTypeRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Person type ID is required.")
                .MustAsync(ExistAsync).WithMessage("Person type with the specified ID does not exist.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type must not exceed 100 characters.")
                .MustAsync(async (command, typeName, cancellationToken) =>
                {
                    var existingTypes = await _personTypeRepository.GetByTypeAsync(typeName, cancellationToken);
                    return !existingTypes.Any() || existingTypes.Count() == 1 && existingTypes.First().Id == command.Id;
                }).WithMessage("A person type with the same name already exists.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

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