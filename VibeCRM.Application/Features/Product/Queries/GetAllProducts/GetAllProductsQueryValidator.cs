using FluentValidation;

namespace VibeCRM.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Validator for the GetAllProductsQuery class
    /// </summary>
    public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProductsQueryValidator"/> class.
        /// </summary>
        /// <remarks>
        /// No validation rules are needed for this query as it doesn't have any parameters.
        /// </remarks>
        public GetAllProductsQueryValidator()
        {
            // No validation rules needed for this query
        }
    }
}