using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a company in the system
    /// </summary>
    public class Company : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the Company class
        /// </summary>
        public Company()
        {
            // Initialize collections
            Addresses = new HashSet<Company_Address>();
            Phones = new HashSet<Company_Phone>();
            EmailAddresses = new HashSet<Company_EmailAddress>();
            Notes = new HashSet<Company_Note>();
            Attachments = new HashSet<Company_Attachment>();
            Activities = new HashSet<Company_Activity>();
            People = new HashSet<Company_Person>();
            Quotes = new HashSet<Company_Quote>();
            SalesOrders = new HashSet<Company_SalesOrder>();
            Calls = new HashSet<Company_Call>();
            ChildCompanies = new List<Company>();
            Id = Guid.NewGuid();
            Name = string.Empty;
        }

        /// <summary>
        /// Gets or sets the company identifier that directly maps to the CompanyId database column
        /// </summary>
        public Guid CompanyId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the parent company identifier
        /// </summary>
        public Guid? ParentCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the company description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the account type identifier
        /// </summary>
        public Guid AccountTypeId { get; set; }

        /// <summary>
        /// Gets or sets the account status identifier
        /// </summary>
        public Guid AccountStatusId { get; set; }

        /// <summary>
        /// Gets or sets the primary contact identifier
        /// </summary>
        public Guid PrimaryContactId { get; set; }

        /// <summary>
        /// Gets or sets the primary phone identifier
        /// </summary>
        public Guid PrimaryPhoneId { get; set; }

        /// <summary>
        /// Gets or sets the primary address identifier
        /// </summary>
        public Guid PrimaryAddressId { get; set; }

        /// <summary>
        /// Gets or sets the company website
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// Gets or sets the parent company
        /// </summary>
        public Company? ParentCompany { get; set; }

        /// <summary>
        /// Gets or sets the account type
        /// </summary>
        public AccountType? AccountType { get; set; }

        /// <summary>
        /// Gets or sets the account status. May be null if not assigned yet.
        /// </summary>
        public AccountStatus? AccountStatus { get; set; }

        /// <summary>
        /// Gets or sets the primary contact. May be null if not assigned yet.
        /// </summary>
        public Person? PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets the primary phone. May be null if not assigned yet.
        /// </summary>
        public Phone? PrimaryPhone { get; set; }

        /// <summary>
        /// Gets or sets the primary address. May be null if not assigned yet.
        /// </summary>
        public Address? PrimaryAddress { get; set; }

        /// <summary>
        /// Gets or sets the collection of addresses associated with this company
        /// </summary>
        public ICollection<Company_Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of phone numbers associated with this company
        /// </summary>
        public ICollection<Company_Phone> Phones { get; set; }

        /// <summary>
        /// Gets or sets the collection of email addresses associated with this company
        /// </summary>
        public ICollection<Company_EmailAddress> EmailAddresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of notes associated with this company
        /// </summary>
        public ICollection<Company_Note> Notes { get; set; }

        /// <summary>
        /// Gets or sets the collection of attachments associated with this company
        /// </summary>
        public ICollection<Company_Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the collection of activities associated with this company
        /// </summary>
        public ICollection<Company_Activity> Activities { get; set; }

        /// <summary>
        /// Gets or sets the collection of people associated with this company
        /// </summary>
        public ICollection<Company_Person> People { get; set; }

        /// <summary>
        /// Gets or sets the collection of quotes associated with this company
        /// </summary>
        public ICollection<Company_Quote> Quotes { get; set; }

        /// <summary>
        /// Gets or sets the collection of sales orders associated with this company
        /// </summary>
        public ICollection<Company_SalesOrder> SalesOrders { get; set; }

        /// <summary>
        /// Gets or sets the collection of calls associated with this company
        /// </summary>
        public ICollection<Company_Call> Calls { get; set; }

        /// <summary>
        /// Gets or sets the collection of child companies
        /// </summary>
        public ICollection<Company> ChildCompanies { get; set; }
    }
}