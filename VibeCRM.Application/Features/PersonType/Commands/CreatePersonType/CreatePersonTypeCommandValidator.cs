using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Application.Features.PersonType.Commands.CreatePersonType;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Commands.CreatePersonType
{
    /// <summary>
    /// Validator for the CreatePersonTypeCommand.
    /// Ensures that all required fields are provided and valid before processing the command.
    /// </summary>
    public class CreatePersonTypeCommandValidator : AbstractValidator<CreatePersonTypeCommand>
    {
        private readonly IPersonTypeRepository _personTypeRepository;

        /// <summary>
        /// Initializes a new instance of the CreatePersonTypeCommandValidator class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations, used for validation checks.</param>
        public CreatePersonTypeCommandValidator(IPersonTypeRepository personTypeRepository)
        {
            _personTypeRepository = personTypeRepository;

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type must not exceed 100 characters.")
                .MustAsync(BeUniqueType).WithMessage("A person type with the same name already exists.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");
        }

        /// <summary>
        /// Validates that the type name is unique among active person types.
        /// </summary>
        /// <param name="typeName">The type name to check for uniqueness.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the type name is unique; otherwise, false.</returns>
        private async Task<bool> BeUniqueType(string typeName, CancellationToken cancellationToken)
        {
            var existingTypes = await _personTypeRepository.GetByTypeAsync(typeName, cancellationToken);
            return !existingTypes.Any();
        }
    }
}