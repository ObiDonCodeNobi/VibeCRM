using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Queries.GetPersonTypeById
{
    /// <summary>
    /// Validator for the GetPersonTypeByIdQuery.
    /// Ensures that the person type ID is valid before processing the query.
    /// </summary>
    public class GetPersonTypeByIdQueryValidator : AbstractValidator<GetPersonTypeByIdQuery>
    {
        private readonly IPersonTypeRepository _personTypeRepository;

        /// <summary>
        /// Initializes a new instance of the GetPersonTypeByIdQueryValidator class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations, used for validation checks.</param>
        public GetPersonTypeByIdQueryValidator(IPersonTypeRepository personTypeRepository)
        {
            _personTypeRepository = personTypeRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Person type ID is required.")
                .MustAsync(ExistAsync).WithMessage("Person type with the specified ID does not exist.");
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