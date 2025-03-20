using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a call in the CRM system
    /// </summary>
    public class Call : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Call"/> class.
        /// </summary>
        public Call()
        {
            Companies = new HashSet<Company_Call>();
            Persons = new HashSet<Person_Call>();
            Id = Guid.NewGuid();
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the call identifier that directly maps to the CallId database column
        /// </summary>
        public Guid CallId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the call type identifier
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Gets or sets the call status identifier
        /// </summary>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Gets or sets the call direction identifier
        /// </summary>
        public Guid DirectionId { get; set; }

        /// <summary>
        /// Gets or sets the description of the call
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the duration of the call in seconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets a value indicating whether this call was inbound (computed property based on DirectionId)
        /// </summary>
        public bool IsInbound => DirectionId != Guid.Empty; // This is a placeholder - the actual logic would depend on the specific DirectionId that represents inbound calls

        /// <summary>
        /// Gets or sets the collection of companies associated with this call
        /// </summary>
        public ICollection<Company_Call> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of persons associated with this call
        /// </summary>
        public ICollection<Person_Call> Persons { get; set; }
    }
}