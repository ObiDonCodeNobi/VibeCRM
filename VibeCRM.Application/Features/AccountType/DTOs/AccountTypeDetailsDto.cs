namespace VibeCRM.Application.Features.AccountType.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed AccountType information.
    /// Contains the core properties of an account type along with related data for detailed views.
    /// </summary>
    public class AccountTypeDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Customer", "Vendor", "Partner").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the account type.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting account types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of companies associated with this account type.
        /// </summary>
        public int CompanyCount { get; set; }

        /// <summary>
        /// Gets or sets the date when the account type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the account type.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the account type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the account type.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}