namespace VibeCRM.Shared.DTOs.AccountStatus
{
    /// <summary>
    /// Detailed DTO for transferring comprehensive account status data.
    /// Extends the base AccountStatusDto with additional properties.
    /// </summary>
    public class AccountStatusDetailsDto : AccountStatusDto
    {
        /// <summary>
        /// Gets or sets the ID of the user who created the account status.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account status was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the account status.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account status was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the account status is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the number of companies using this account status.
        /// </summary>
        public int CompanyCount { get; set; }
    }
}