using FluentValidation;

namespace VibeCRM.Application.Features.ProductType.Queries.GetAllProductTypes
{
    /// <summary>
    /// Validator for the GetAllProductTypesQuery class.
    /// Defines validation rules for retrieving all product types.
    /// </summary>
    public class GetAllProductTypesQueryValidator : AbstractValidator<GetAllProductTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProductTypesQueryValidator"/> class.
        /// No specific validation rules are needed for this query as it doesn't have any parameters.
        /// </summary>
        public GetAllProductTypesQueryValidator()
        {
            // No specific validation rules are needed for this query
            // as it doesn't have any parameters
        }
    }
}
