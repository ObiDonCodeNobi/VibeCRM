using FluentValidation;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetRootProductGroups
{
    /// <summary>
    /// Validator for the <see cref="GetRootProductGroupsQuery"/> class.
    /// Defines validation rules for retrieving all root-level product groups.
    /// </summary>
    public class GetRootProductGroupsQueryValidator : AbstractValidator<GetRootProductGroupsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRootProductGroupsQueryValidator"/> class.
        /// Since GetRootProductGroupsQuery has no properties to validate, this validator contains no rules.
        /// It's included for consistency with the validation pattern used throughout the application.
        /// </summary>
        public GetRootProductGroupsQueryValidator()
        {
            // No validation rules needed as GetRootProductGroupsQuery has no properties
        }
    }
}