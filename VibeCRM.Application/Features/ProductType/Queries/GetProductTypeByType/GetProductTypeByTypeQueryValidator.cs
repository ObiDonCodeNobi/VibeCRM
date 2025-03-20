using FluentValidation;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeByType
{
    /// <summary>
    /// Validator for the GetProductTypeByTypeQuery class.
    /// Defines validation rules for retrieving a product type by its type name.
    /// </summary>
    public class GetProductTypeByTypeQueryValidator : AbstractValidator<GetProductTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductTypeByTypeQueryValidator"/> class.
        /// Configures validation rules for GetProductTypeByTypeQuery properties.
        /// </summary>
        public GetProductTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Product type name is required.")
                .MaximumLength(50)
                .WithMessage("Product type name cannot exceed 50 characters.");
        }
    }
}