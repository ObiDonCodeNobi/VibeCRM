using FluentValidation;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetAllProductGroups
{
    /// <summary>
    /// Validator for the <see cref="GetAllProductGroupsQuery"/> class.
    /// Defines validation rules for retrieving all product groups.
    /// </summary>
    public class GetAllProductGroupsQueryValidator : AbstractValidator<GetAllProductGroupsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProductGroupsQueryValidator"/> class.
        /// Since GetAllProductGroupsQuery has no properties to validate, this validator contains no rules.
        /// It's included for consistency with the validation pattern used throughout the application.
        /// </summary>
        public GetAllProductGroupsQueryValidator()
        {
            // No validation rules needed as GetAllProductGroupsQuery has no properties
        }
    }
}