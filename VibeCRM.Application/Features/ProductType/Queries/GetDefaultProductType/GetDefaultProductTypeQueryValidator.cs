using FluentValidation;

namespace VibeCRM.Application.Features.ProductType.Queries.GetDefaultProductType
{
    /// <summary>
    /// Validator for the GetDefaultProductTypeQuery class.
    /// Defines validation rules for retrieving the default product type.
    /// </summary>
    public class GetDefaultProductTypeQueryValidator : AbstractValidator<GetDefaultProductTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultProductTypeQueryValidator"/> class.
        /// No specific validation rules are needed for this query as it doesn't have any parameters.
        /// </summary>
        public GetDefaultProductTypeQueryValidator()
        {
            // No specific validation rules are needed for this query
            // as it doesn't have any parameters
        }
    }
}
