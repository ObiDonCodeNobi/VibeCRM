namespace VibeCRM.Shared.DTOs.Company
{
    /// <summary>
    /// Base DTO for transferring company data between layers.
    /// Contains the essential properties of a company.
    /// </summary>
    public class CompanyDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the company.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the parent company identifier.
        /// </summary>
        public Guid? ParentCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the company description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the account type identifier.
        /// </summary>
        public Guid AccountTypeId { get; set; }

        /// <summary>
        /// Gets or sets the account status identifier.
        /// </summary>
        public Guid AccountStatusId { get; set; }

        /// <summary>
        /// Gets or sets the primary contact identifier.
        /// </summary>
        public Guid PrimaryContactId { get; set; }

        /// <summary>
        /// Gets or sets the primary phone identifier.
        /// </summary>
        public Guid PrimaryPhoneId { get; set; }

        /// <summary>
        /// Gets or sets the primary address identifier.
        /// </summary>
        public Guid PrimaryAddressId { get; set; }

        /// <summary>
        /// Gets or sets the company website.
        /// </summary>
        public string? Website { get; set; }
    }
}