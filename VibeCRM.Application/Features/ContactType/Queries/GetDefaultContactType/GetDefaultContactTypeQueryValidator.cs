using FluentValidation;

namespace VibeCRM.Application.Features.ContactType.Queries.GetDefaultContactType
{
    /// <summary>
    /// Validator for the <see cref="GetDefaultContactTypeQuery"/> class.
    /// </summary>
    public class GetDefaultContactTypeQueryValidator : AbstractValidator<GetDefaultContactTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultContactTypeQueryValidator"/> class.
        /// </summary>
        public GetDefaultContactTypeQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}