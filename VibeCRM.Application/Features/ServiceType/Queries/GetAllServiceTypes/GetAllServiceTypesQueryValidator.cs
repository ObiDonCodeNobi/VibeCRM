using FluentValidation;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetAllServiceTypes
{
    /// <summary>
    /// Validator for the GetAllServiceTypesQuery.
    /// Defines validation rules for retrieving all service types.
    /// </summary>
    public class GetAllServiceTypesQueryValidator : AbstractValidator<GetAllServiceTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllServiceTypesQueryValidator"/> class.
        /// Since GetAllServiceTypesQuery has no properties to validate, this validator has no rules.
        /// </summary>
        public GetAllServiceTypesQueryValidator()
        {
            // No properties to validate in GetAllServiceTypesQuery
        }
    }
}
