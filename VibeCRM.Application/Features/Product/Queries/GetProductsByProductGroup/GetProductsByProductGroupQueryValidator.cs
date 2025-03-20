using FluentValidation;

namespace VibeCRM.Application.Features.Product.Queries.GetProductsByProductGroup
{
    /// <summary>
    /// Validator for the GetProductsByProductGroupQuery class
    /// </summary>
    public class GetProductsByProductGroupQueryValidator : AbstractValidator<GetProductsByProductGroupQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductsByProductGroupQueryValidator"/> class with validation rules.
        /// </summary>
        public GetProductsByProductGroupQueryValidator()
        {
            RuleFor(q => q.ProductGroupId)
                .NotEmpty().WithMessage("Product Group ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Product Group ID cannot be empty.");
        }
    }
}