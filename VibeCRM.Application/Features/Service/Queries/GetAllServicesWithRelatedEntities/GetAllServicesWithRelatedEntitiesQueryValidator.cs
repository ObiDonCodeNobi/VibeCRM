using FluentValidation;

namespace VibeCRM.Application.Features.Service.Queries.GetAllServicesWithRelatedEntities
{
    /// <summary>
    /// Validator for the <see cref="GetAllServicesWithRelatedEntitiesQuery"/>
    /// </summary>
    public class GetAllServicesWithRelatedEntitiesQueryValidator : AbstractValidator<GetAllServicesWithRelatedEntitiesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllServicesWithRelatedEntitiesQueryValidator"/> class
        /// </summary>
        public GetAllServicesWithRelatedEntitiesQueryValidator()
        {
            // No validation rules needed for this query as it doesn't have any parameters
        }
    }
}