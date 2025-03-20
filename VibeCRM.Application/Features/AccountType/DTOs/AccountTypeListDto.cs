namespace VibeCRM.Application.Features.AccountType.DTOs
{
    /// <summary>
    /// Data Transfer Object for AccountType entities in list views.
    /// Contains the core properties of an account type along with additional information for list displays.
    /// </summary>
    public class AccountTypeListDto
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
    }
}