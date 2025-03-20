using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ProductGroup.Commands.DeleteProductGroup
{
    /// <summary>
    /// Validator for the <see cref="DeleteProductGroupCommand"/> class.
    /// Defines validation rules for deleting product groups.
    /// </summary>
    public class DeleteProductGroupCommandValidator : AbstractValidator<DeleteProductGroupCommand>
    {
        private readonly IProductGroupRepository _productGroupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductGroupCommandValidator"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for validation checks.</param>
        public DeleteProductGroupCommandValidator(IProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product group ID is required.")
                .MustAsync(BeExistingProductGroupId).WithMessage("The specified product group does not exist or is already deleted.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier information is required.");
        }

        /// <summary>
        /// Validates that the product group ID exists and is active.
        /// </summary>
        /// <param name="id">The ID to validate.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the product group exists and is active, false otherwise.</returns>
        private async Task<bool> BeExistingProductGroupId(Guid id, CancellationToken cancellationToken)
        {
            var existingProductGroup = await _productGroupRepository.GetByIdAsync(id, cancellationToken);
            return existingProductGroup != null && existingProductGroup.Active;
        }
    }
}