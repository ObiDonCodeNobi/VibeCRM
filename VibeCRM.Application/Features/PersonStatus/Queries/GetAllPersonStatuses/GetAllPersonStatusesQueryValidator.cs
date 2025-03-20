using FluentValidation;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetAllPersonStatuses
{
    /// <summary>
    /// Validator for the GetAllPersonStatusesQuery.
    /// Defines validation rules for retrieving all person statuses queries.
    /// </summary>
    public class GetAllPersonStatusesQueryValidator : AbstractValidator<GetAllPersonStatusesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPersonStatusesQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetAllPersonStatusesQueryValidator()
        {
            // No specific validation rules needed for this query
            // The IncludeInactive property is a boolean with a default value
        }
    }
}
