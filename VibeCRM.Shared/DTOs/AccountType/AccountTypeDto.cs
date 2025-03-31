namespace VibeCRM.Shared.DTOs.AccountType
{
    /// <summary>
    /// Data Transfer Object for AccountType entities.
    /// Contains the core properties of an account type.
    /// </summary>
    public class AccountTypeDto
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
    }
}