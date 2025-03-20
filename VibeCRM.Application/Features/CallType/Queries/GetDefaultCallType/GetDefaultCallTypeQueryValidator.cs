using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Queries.GetDefaultCallType
{
    /// <summary>
    /// Validator for the GetDefaultCallTypeQuery.
    /// Defines validation rules for retrieving the default call type.
    /// </summary>
    public class GetDefaultCallTypeQueryValidator : AbstractValidator<GetDefaultCallTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultCallTypeQueryValidator"/> class.
        /// Sets up validation rules for the GetDefaultCallTypeQuery.
        /// </summary>
        public GetDefaultCallTypeQueryValidator()
        {
            // No specific validation rules needed for this query as it has no parameters
        }
    }
}