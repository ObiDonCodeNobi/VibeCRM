using FluentValidation;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetProductTypeByOrdinalPositionQuery class.
    /// Defines validation rules for retrieving a product type by its ordinal position.
    /// </summary>
    public class GetProductTypeByOrdinalPositionQueryValidator : AbstractValidator<GetProductTypeByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductTypeByOrdinalPositionQueryValidator"/> class.
        /// Configures validation rules for GetProductTypeByOrdinalPositionQuery properties.
        /// </summary>
        public GetProductTypeByOrdinalPositionQueryValidator()
        {
            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}
