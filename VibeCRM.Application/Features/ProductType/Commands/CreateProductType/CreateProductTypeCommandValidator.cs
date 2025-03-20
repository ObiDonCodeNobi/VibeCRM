using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Commands.CreateProductType
{
    /// <summary>
    /// Validator for the CreateProductTypeCommand class.
    /// Defines validation rules for creating a new product type.
    /// </summary>
    public class CreateProductTypeCommandValidator : AbstractValidator<CreateProductTypeCommand>
    {
        private readonly IProductTypeRepository _productTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductTypeCommandValidator"/> class.
        /// Configures validation rules for CreateProductTypeCommand properties.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository for validating uniqueness.</param>
        public CreateProductTypeCommandValidator(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Product type name is required.")
                .MaximumLength(50)
                .WithMessage("Product type name cannot exceed 50 characters.")
                .MustAsync(async (type, cancellation) =>
                {
                    var existingTypes = await _productTypeRepository.GetByTypeAsync(type, cancellation);
                    return !existingTypes.Any();
                })
                .WithMessage("A product type with this name already exists.");

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