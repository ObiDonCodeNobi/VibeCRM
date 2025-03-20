using FluentValidation;

namespace VibeCRM.Application.Features.ContactType.Queries.GetAllContactTypes
{
    /// <summary>
    /// Validator for the <see cref="GetAllContactTypesQuery"/> class.
    /// </summary>
    public class GetAllContactTypesQueryValidator : AbstractValidator<GetAllContactTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllContactTypesQueryValidator"/> class.
        /// </summary>
        public GetAllContactTypesQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}