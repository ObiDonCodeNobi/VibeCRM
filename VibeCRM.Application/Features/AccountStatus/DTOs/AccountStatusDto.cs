namespace VibeCRM.Application.Features.AccountStatus.DTOs
{
    /// <summary>
    /// Base DTO for transferring account status data.
    /// Contains the core properties that represent an account status.
    /// </summary>
    public class AccountStatusDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account status.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Active", "Inactive", "Prospect").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the description of the account status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting account statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        public AccountStatusDto()
        { Status = string.Empty; Description = string.Empty; }
    }
}