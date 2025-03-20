using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeById
{
    /// <summary>
    /// Validator for the GetProductTypeByIdQuery class.
    /// Defines validation rules for retrieving a product type by its ID.
    /// </summary>
    public class GetProductTypeByIdQueryValidator : AbstractValidator<GetProductTypeByIdQuery>
    {
        private readonly IProductTypeRepository _productTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductTypeByIdQueryValidator"/> class.
        /// Configures validation rules for GetProductTypeByIdQuery properties.
        /// </summary>
        /// <param name="productTypeRepository">The product type repository for validating existence.</param>
        public GetProductTypeByIdQueryValidator(IProductTypeRepository productTypeRepository)
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