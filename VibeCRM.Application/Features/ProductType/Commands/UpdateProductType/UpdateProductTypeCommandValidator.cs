using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Commands.UpdateProductType
{
    /// <summary>
    /// Validator for the UpdateProductTypeCommand class.
    /// Defines validation rules for updating an existing product type.
    /// </summary>
    public class UpdateProductTypeCommandValidator : AbstractValidator<UpdateProductTypeCommand>
    {
        private readonly IProductTypeRepository _productTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductTypeCommandValidator"/> class.
        /// Configures validation rules for UpdateProductTypeCommand properties.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository for validating existence and uniqueness.</param>
        public UpdateProductTypeCommandValidator(IProductTypeRepository productTypeRepository)
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

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Product type name is required.")
                .MaximumLength(50)
                .WithMessage("Product type name cannot exceed 50 characters.")
                .MustAsync(async (command, type, cancellation) =>
                {
                    var existingTypes = await _productTypeRepository.GetByTypeAsync(type, cancellation);
                    return !existingTypes.Any(pt => pt.Id != command.Id);
                })
                .WithMessage("Another product type with this name already exists.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}