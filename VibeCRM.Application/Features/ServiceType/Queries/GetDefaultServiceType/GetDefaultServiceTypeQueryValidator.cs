using FluentValidation;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetDefaultServiceType
{
    /// <summary>
    /// Validator for the GetDefaultServiceTypeQuery.
    /// Defines validation rules for retrieving the default service type.
    /// </summary>
    public class GetDefaultServiceTypeQueryValidator : AbstractValidator<GetDefaultServiceTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultServiceTypeQueryValidator"/> class.
        /// Since GetDefaultServiceTypeQuery has no properties to validate, this validator has no rules.
        /// </summary>
        public GetDefaultServiceTypeQueryValidator()
        {
            // No properties to validate in GetDefaultServiceTypeQuery
        }
    }
}
