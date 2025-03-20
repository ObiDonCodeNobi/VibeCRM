using FluentValidation;
using System;

namespace VibeCRM.Application.Features.Product.Commands.DeleteProduct
{
    /// <summary>
    /// Validator for the DeleteProductCommand class
    /// </summary>
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductCommandValidator"/> class with validation rules.
        /// </summary>
        public DeleteProductCommandValidator()
        {
            RuleFor(c => c.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(c => c.DeletedBy)
                .NotEmpty().WithMessage("Deleter information is required.")
                .MaximumLength(100).WithMessage("Deleter information cannot exceed 100 characters.");
        }
    }
}
