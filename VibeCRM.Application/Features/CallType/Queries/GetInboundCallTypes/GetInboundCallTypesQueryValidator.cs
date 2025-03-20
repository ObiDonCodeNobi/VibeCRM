using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Queries.GetInboundCallTypes
{
    /// <summary>
    /// Validator for the GetInboundCallTypesQuery.
    /// Defines validation rules for retrieving inbound call types.
    /// </summary>
    public class GetInboundCallTypesQueryValidator : AbstractValidator<GetInboundCallTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetInboundCallTypesQueryValidator"/> class.
        /// Sets up validation rules for the GetInboundCallTypesQuery.
        /// </summary>
        public GetInboundCallTypesQueryValidator()
        {
            // No specific validation rules needed for this query as it has no parameters
        }
    }
}