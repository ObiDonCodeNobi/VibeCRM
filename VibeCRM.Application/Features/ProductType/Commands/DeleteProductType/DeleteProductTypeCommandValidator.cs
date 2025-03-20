using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Commands.DeleteProductType
{
    /// <summary>
    /// Validator for the DeleteProductTypeCommand class.
    /// Defines validation rules for deleting an existing product type.
    /// </summary>
    public class DeleteProductTypeCommandValidator : AbstractValidator<DeleteProductTypeCommand>
    {
        private readonly IProductTypeRepository _productTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductTypeCommandValidator"/> class.
        /// Configures validation rules for DeleteProductTypeCommand properties.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository for validating existence.</param>
        public DeleteProductTypeCommandValidator(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product type ID is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    var productType = await _productTypeRepository.GetByIdAsync(id, cancellation);
                    return productType != null;
                })
                .WithMessage("Product type with the specified ID does not exist.");
        }
    }
}