using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ProductGroup.Commands.CreateProductGroup
{
    /// <summary>
    /// Validator for the <see cref="CreateProductGroupCommand"/> class.
    /// Defines validation rules for creating product groups.
    /// </summary>
    public class CreateProductGroupCommandValidator : AbstractValidator<CreateProductGroupCommand>
    {
        private readonly IProductGroupRepository _productGroupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductGroupCommandValidator"/> class.
        /// </summary>
        /// <param name="productGroupRepository">The product group repository for validation checks.</param>
        public CreateProductGroupCommandValidator(IProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product group name is required.")
                .MaximumLength(100).WithMessage("Product group name cannot exceed 100 characters.")
                .MustAsync(BeUniqueName).WithMessage("A product group with this name already exists.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product group description is required.")
                .MaximumLength(500).WithMessage("Product group description cannot exceed 500 characters.");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Display order must be a non-negative number.");

            RuleFor(x => x.ParentProductGroupId)
                .MustAsync(BeExistingProductGroupIdOrNull).WithMessage("The specified parent product group does not exist.")
                .When(x => x.ParentProductGroupId.HasValue);

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator information is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier information is required.");
        }

        /// <summary>
        /// Validates that the product group name is unique.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the name is unique, false otherwise.</returns>
        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            var existingProductGroup = await _productGroupRepository.GetByNameAsync(name, cancellationToken);
            return existingProductGroup == null;
        }

        /// <summary>
        /// Validates that the parent product group ID exists or is null.
        /// </summary>
        /// <param name="parentProductGroupId">The parent product group ID to validate.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the parent product group exists or is null, false otherwise.</returns>
        private async Task<bool> BeExistingProductGroupIdOrNull(Guid? parentProductGroupId, CancellationToken cancellationToken)
        {
            if (!parentProductGroupId.HasValue)
                return true;

            var existingProductGroup = await _productGroupRepository.GetByIdAsync(parentProductGroupId.Value, cancellationToken);
            return existingProductGroup != null;
        }
    }
}