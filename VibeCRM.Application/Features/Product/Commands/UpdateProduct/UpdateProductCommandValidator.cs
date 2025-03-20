using FluentValidation;

namespace VibeCRM.Application.Features.Product.Commands.UpdateProduct
{
    /// <summary>
    /// Validator for the UpdateProductCommand class
    /// </summary>
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommandValidator"/> class with validation rules.
        /// </summary>
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(c => c.ProductTypeId)
                .NotEmpty().WithMessage("Product Type ID is required.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modifier information is required.")
                .MaximumLength(100).WithMessage("Modifier information cannot exceed 100 characters.");
        }
    }
}