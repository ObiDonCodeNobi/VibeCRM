namespace VibeCRM.Application.Features.AccountStatus.DTOs
{
    /// <summary>
    /// List DTO for transferring account status data in list views.
    /// Contains a subset of properties optimized for display in lists.
    /// </summary>
    public class AccountStatusListDto : AccountStatusDto
    {
        /// <summary>
        /// Gets or sets the date and time when the account status was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the number of companies using this account status.
        /// </summary>
        public int CompanyCount { get; set; }
    }
}