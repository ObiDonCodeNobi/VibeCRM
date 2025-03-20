using FluentValidation;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAllAccountStatuses
{
    /// <summary>
    /// Validator for the GetAllAccountStatusesQuery.
    /// Defines validation rules for retrieving all account statuses queries.
    /// </summary>
    public class GetAllAccountStatusesQueryValidator : AbstractValidator<GetAllAccountStatusesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAccountStatusesQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetAllAccountStatusesQueryValidator()
        {
            // No specific validation rules needed for this query
            // as it doesn't have any parameters
        }
    }
}