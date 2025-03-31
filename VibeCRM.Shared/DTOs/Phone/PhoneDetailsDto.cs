namespace VibeCRM.Shared.DTOs.Phone
{
    /// <summary>
    /// Detailed Data Transfer Object for Phone entity with related entities information
    /// </summary>
    public class PhoneDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the phone
        /// </summary>
        public Guid Id { get; set; }

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
        /// Gets or sets the phone type name
        /// </summary>
        public string PhoneTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the formatted phone number
        /// </summary>
        public string FormattedPhoneNumber => $"({AreaCode}) {Prefix}-{LineNumber}{(Extension.HasValue ? $" x{Extension}" : "")}";

        /// <summary>
        /// Gets or sets the collection of associated companies
        /// </summary>
        public ICollection<CompanyPhoneDto> Companies { get; set; } = new List<CompanyPhoneDto>();

        /// <summary>
        /// Gets or sets the collection of associated persons
        /// </summary>
        public ICollection<PersonPhoneDto> Persons { get; set; } = new List<PersonPhoneDto>();

        /// <summary>
        /// Gets or sets the created by user identifier
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the creation date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified by user identifier
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last modification date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for Company-Phone relationship
    /// </summary>
    public class CompanyPhoneDto
    {
        /// <summary>
        /// Gets or sets the company identifier
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company name
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object for Person-Phone relationship
    /// </summary>
    public class PersonPhoneDto
    {
        /// <summary>
        /// Gets or sets the person identifier
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person's full name
        /// </summary>
        public string FullName { get; set; } = string.Empty;
    }
}