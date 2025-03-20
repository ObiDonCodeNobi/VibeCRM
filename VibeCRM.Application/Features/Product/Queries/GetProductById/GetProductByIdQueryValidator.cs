using FluentValidation;

namespace VibeCRM.Application.Features.Product.Queries.GetProductById
{
    /// <summary>
    /// Validator for the GetProductByIdQuery class
    /// </summary>
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductByIdQueryValidator"/> class with validation rules.
        /// </summary>
        public GetProductByIdQueryValidator()
        {
            RuleFor(q => q.ProductId)
                .NotEmpty().WithMessage("Product ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Product ID cannot be empty.");
        }
    }
}