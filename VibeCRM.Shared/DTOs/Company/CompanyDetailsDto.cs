namespace VibeCRM.Shared.DTOs.Company
{
    /// <summary>
    /// Detailed DTO for transferring comprehensive company data.
    /// Extends the base CompanyDto with additional properties.
    /// </summary>
    public class CompanyDetailsDto : CompanyDto
    {
        /// <summary>
        /// Gets or sets the parent company name.
        /// </summary>
        public string? ParentCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the account type name.
        /// </summary>
        public string? AccountTypeName { get; set; }

        /// <summary>
        /// Gets or sets the account status name.
        /// </summary>
        public string? AccountStatusName { get; set; }

        /// <summary>
        /// Gets or sets the primary contact name.
        /// </summary>
        public string? PrimaryContactName { get; set; }

        /// <summary>
        /// Gets or sets the primary phone number.
        /// </summary>
        public string? PrimaryPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the primary address formatted as a single line.
        /// </summary>
        public string? PrimaryAddressFormatted { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the company.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the company was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the company.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the company was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the company is active.
        /// </summary>
        public bool Active { get; set; }
    }
}