namespace VibeCRM.Shared.DTOs.EmailAddress
{
    /// <summary>
    /// List DTO for transferring email address data in list views.
    /// Contains a subset of properties optimized for display in lists.
    /// </summary>
    public class EmailAddressListDto : EmailAddressDto
    {
        /// <summary>
        /// Gets or sets the email address type name.
        /// </summary>
        public string? EmailAddressTypeName { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the email address was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}