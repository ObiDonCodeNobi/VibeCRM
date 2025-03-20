using FluentValidation;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetDefaultCallDirection
{
    /// <summary>
    /// Validator for the GetDefaultCallDirectionQuery.
    /// Included for consistency with the validation pattern across the application.
    /// </summary>
    public class GetDefaultCallDirectionQueryValidator : AbstractValidator<GetDefaultCallDirectionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultCallDirectionQueryValidator"/> class.
        /// No specific rules are defined as the query has no parameters.
        /// </summary>
        public GetDefaultCallDirectionQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}