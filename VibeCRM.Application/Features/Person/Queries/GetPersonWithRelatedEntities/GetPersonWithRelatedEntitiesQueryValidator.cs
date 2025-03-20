using FluentValidation;

namespace VibeCRM.Application.Features.Person.Queries.GetPersonWithRelatedEntities
{
    /// <summary>
    /// Validator for the GetPersonWithRelatedEntitiesQuery.
    /// Validates that the query parameters meet the required criteria.
    /// </summary>
    public class GetPersonWithRelatedEntitiesQueryValidator : AbstractValidator<GetPersonWithRelatedEntitiesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonWithRelatedEntitiesQueryValidator"/> class.
        /// Defines validation rules for the GetPersonWithRelatedEntitiesQuery.
        /// </summary>
        public GetPersonWithRelatedEntitiesQueryValidator()
        {
            // Validate that the person ID is not empty
            RuleFor(query => query.Id)
                .NotEmpty()
                .WithMessage("Person ID cannot be empty.");

            // At least one related entity should be requested
            RuleFor(query => new
            {
                query.LoadCompanies,
                query.LoadAddresses,
                query.LoadPhoneNumbers,
                query.LoadEmailAddresses,
                query.LoadActivities,
                query.LoadAttachments,
                query.LoadNotes,
                query.LoadCalls
            })
            .Must(x => x.LoadCompanies || x.LoadAddresses || x.LoadPhoneNumbers || 
                       x.LoadEmailAddresses || x.LoadActivities || x.LoadAttachments || 
                       x.LoadNotes || x.LoadCalls)
            .WithMessage("At least one related entity must be requested to load.");
        }
    }
}
