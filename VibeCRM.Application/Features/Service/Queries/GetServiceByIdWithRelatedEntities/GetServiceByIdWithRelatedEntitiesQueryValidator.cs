using FluentValidation;

namespace VibeCRM.Application.Features.Service.Queries.GetServiceByIdWithRelatedEntities
{
    /// <summary>
    /// Validator for the <see cref="GetServiceByIdWithRelatedEntitiesQuery"/>
    /// </summary>
    public class GetServiceByIdWithRelatedEntitiesQueryValidator : AbstractValidator<GetServiceByIdWithRelatedEntitiesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceByIdWithRelatedEntitiesQueryValidator"/> class
        /// </summary>
        public GetServiceByIdWithRelatedEntitiesQueryValidator()
        {
            RuleFor(query => query.Id)
                .NotEmpty()
                .WithMessage("Service ID is required")
                .WithErrorCode("SERVICE_ID_REQUIRED");
        }
    }
}