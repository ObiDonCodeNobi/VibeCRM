using FluentValidation;

namespace VibeCRM.Application.Features.PersonType.Queries.GetAllPersonTypes
{
    /// <summary>
    /// Validator for the GetAllPersonTypesQuery.
    /// Since this query doesn't have any parameters, the validator is minimal.
    /// </summary>
    public class GetAllPersonTypesQueryValidator : AbstractValidator<GetAllPersonTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllPersonTypesQueryValidator class.
        /// </summary>
        public GetAllPersonTypesQueryValidator()
        {
            // No validation rules needed as the query doesn't have any parameters
        }
    }
}