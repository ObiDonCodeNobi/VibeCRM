using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a phone number in the CRM system
    /// </summary>
    public class Phone : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Phone"/> class.
        /// </summary>
        public Phone()
        {
            Companies = new HashSet<Company_Phone>();
            Persons = new HashSet<Person_Phone>();
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the phone identifier that directly maps to the PhoneId database column
        /// </summary>
        public Guid PhoneId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the area code
        /// </summary>
        public int AreaCode { get; set; }

        /// <summary>
        /// Gets or sets the prefix
        /// </summary>
        public int Prefix { get; set; }

        /// <summary>
        /// Gets or sets the line number
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the extension
        /// </summary>
        public int? Extension { get; set; }

        /// <summary>
        /// Gets or sets the phone type identifier
        /// </summary>
        public Guid PhoneTypeId { get; set; }

        /// <summary>
        /// Gets the formatted phone number (computed property)
        /// </summary>
        public string FormattedPhoneNumber => $"({AreaCode}) {Prefix}-{LineNumber}{(Extension.HasValue ? $" x{Extension}" : "")}";

        /// <summary>
        /// Gets or sets the phone type
        /// </summary>
        public PhoneType? PhoneType { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies associated with this phone
        /// </summary>
        public ICollection<Company_Phone> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of persons associated with this phone
        /// </summary>
        public ICollection<Person_Phone> Persons { get; set; }
    }
}