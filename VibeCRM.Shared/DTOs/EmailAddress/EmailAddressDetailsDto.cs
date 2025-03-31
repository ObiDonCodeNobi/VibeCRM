namespace VibeCRM.Shared.DTOs.EmailAddress
{
    /// <summary>
    /// Detailed DTO for transferring comprehensive email address data.
    /// Extends the base EmailAddressDto with additional properties.
    /// </summary>
    public class EmailAddressDetailsDto : EmailAddressDto
    {
        /// <summary>
        /// Gets or sets the email address type name.
        /// </summary>
        public string? EmailAddressTypeName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the email address.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the email address was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the email address.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the email address was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}