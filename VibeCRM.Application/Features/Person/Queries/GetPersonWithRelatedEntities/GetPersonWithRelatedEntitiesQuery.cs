using MediatR;
using VibeCRM.Application.Features.Person.DTOs;

namespace VibeCRM.Application.Features.Person.Queries.GetPersonWithRelatedEntities
{
    /// <summary>
    /// Query to retrieve a person by ID with specified related entities loaded.
    /// </summary>
    public class GetPersonWithRelatedEntitiesQuery : IRequest<PersonDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load companies associated with the person.
        /// </summary>
        public bool LoadCompanies { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load addresses associated with the person.
        /// </summary>
        public bool LoadAddresses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load phone numbers associated with the person.
        /// </summary>
        public bool LoadPhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load email addresses associated with the person.
        /// </summary>
        public bool LoadEmailAddresses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load activities associated with the person.
        /// </summary>
        public bool LoadActivities { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load attachments associated with the person.
        /// </summary>
        public bool LoadAttachments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load notes associated with the person.
        /// </summary>
        public bool LoadNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to load calls associated with the person.
        /// </summary>
        public bool LoadCalls { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonWithRelatedEntitiesQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the person to retrieve.</param>
        public GetPersonWithRelatedEntitiesQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonWithRelatedEntitiesQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the person to retrieve.</param>
        /// <param name="loadAll">If true, loads all related entities. Otherwise, loads only the specified entities.</param>
        public GetPersonWithRelatedEntitiesQuery(Guid id, bool loadAll)
        {
            Id = id;
            if (loadAll)
            {
                LoadCompanies = true;
                LoadAddresses = true;
                LoadPhoneNumbers = true;
                LoadEmailAddresses = true;
                LoadActivities = true;
                LoadAttachments = true;
                LoadNotes = true;
                LoadCalls = true;
            }
        }
    }
}