namespace VibeCRM.Application.Features.Company.DTOs
{
    /// <summary>
    /// List DTO for transferring company data in list views.
    /// Contains a subset of properties optimized for display in lists.
    /// </summary>
    public class CompanyListDto : CompanyDto
    {
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
        /// Gets or sets the date and time when the company was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}