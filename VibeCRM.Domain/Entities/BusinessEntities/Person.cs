using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a person in the CRM system
    /// </summary>
    public class Person : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            Companies = new HashSet<Company_Person>();
            Addresses = new HashSet<Person_Address>();
            PhoneNumbers = new HashSet<Person_Phone>();
            EmailAddresses = new HashSet<Person_EmailAddress>();
            Activities = new HashSet<Person_Activity>();
            Attachments = new HashSet<Person_Attachment>();
            PersonNotes = new HashSet<Person_Note>();
            Calls = new HashSet<Person_Call>();
            Id = Guid.NewGuid();
            Firstname = string.Empty;
            MiddleInitial = string.Empty;
            Lastname = string.Empty;
            Title = string.Empty;
        }

        /// <summary>
        /// Gets or sets the person identifier that directly maps to the PersonId database column
        /// </summary>
        public Guid PersonId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the first name of the person
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the middle initial of the person
        /// </summary>
        public string MiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets the title of the person
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the full name of the person (computed property)
        /// </summary>
        public string FullName => string.IsNullOrEmpty(MiddleInitial)
            ? $"{Firstname} {Lastname}"
            : $"{Firstname} {MiddleInitial}. {Lastname}";

        /// <summary>
        /// Gets or sets the collection of companies associated with this person
        /// </summary>
        public ICollection<Company_Person> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of addresses associated with this person
        /// </summary>
        public ICollection<Person_Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of phone numbers associated with this person
        /// </summary>
        public ICollection<Person_Phone> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the collection of email addresses associated with this person
        /// </summary>
        public ICollection<Person_EmailAddress> EmailAddresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of activities associated with this person
        /// </summary>
        public ICollection<Person_Activity> Activities { get; set; }

        /// <summary>
        /// Gets or sets the collection of attachments associated with this person
        /// </summary>
        public ICollection<Person_Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the collection of notes associated with this person
        /// </summary>
        public ICollection<Person_Note> PersonNotes { get; set; }

        /// <summary>
        /// Gets or sets the collection of calls associated with this person
        /// </summary>
        public ICollection<Person_Call> Calls { get; set; }
    }
}