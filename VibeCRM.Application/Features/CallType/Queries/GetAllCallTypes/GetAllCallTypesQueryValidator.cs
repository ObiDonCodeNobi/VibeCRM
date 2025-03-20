using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Queries.GetAllCallTypes
{
    /// <summary>
    /// Validator for the GetAllCallTypesQuery.
    /// Defines validation rules for retrieving all call types.
    /// </summary>
    public class GetAllCallTypesQueryValidator : AbstractValidator<GetAllCallTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCallTypesQueryValidator"/> class.
        /// Sets up validation rules for the GetAllCallTypesQuery.
        /// </summary>
        public GetAllCallTypesQueryValidator()
        {
            // No specific validation rules needed for this query as it has no parameters
        }
    }
}