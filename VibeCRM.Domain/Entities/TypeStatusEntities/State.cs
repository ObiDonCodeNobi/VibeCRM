using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a state or province in the system, used for address information.
    /// </summary>
    public class State : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        public State()
        {
            Addresses = new List<Address>();
            Id = Guid.NewGuid();
            Name = string.Empty;
            Abbreviation = string.Empty;
            CountryCode = string.Empty;
        }

        /// <summary>
        /// Gets or sets the state identifier that directly maps to the StateId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid StateId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the name of the state or province.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the state or province abbreviation code (e.g., "CA" for California).
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the country code to which this state belongs (e.g., "US" for United States).
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting states in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the collection of addresses located in this state.
        /// </summary>
        public ICollection<Address> Addresses { get; set; }
    }
}