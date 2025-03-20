using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Queries.GetOutboundCallTypes
{
    /// <summary>
    /// Validator for the GetOutboundCallTypesQuery.
    /// Defines validation rules for retrieving outbound call types.
    /// </summary>
    public class GetOutboundCallTypesQueryValidator : AbstractValidator<GetOutboundCallTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetOutboundCallTypesQueryValidator"/> class.
        /// Sets up validation rules for the GetOutboundCallTypesQuery.
        /// </summary>
        public GetOutboundCallTypesQueryValidator()
        {
            // No specific validation rules needed for this query as it has no parameters
        }
    }
}