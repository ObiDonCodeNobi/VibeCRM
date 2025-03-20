using FluentValidation;

namespace VibeCRM.Application.Features.Product.Commands.CreateProduct
{
    /// <summary>
    /// Validator for the CreateProductCommand class
    /// </summary>
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommandValidator"/> class with validation rules.
        /// </summary>
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.ProductTypeId)
                .NotEmpty().WithMessage("Product Type ID is required.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");

            RuleFor(c => c.CreatedBy)
                .NotEmpty().WithMessage("Creator information is required.")
                .MaximumLength(100).WithMessage("Creator information cannot exceed 100 characters.");
        }
    }
}