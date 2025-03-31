namespace VibeCRM.Shared.DTOs.EmailAddress
{
    /// <summary>
    /// Base DTO for transferring email address data between layers.
    /// Contains the essential properties of an email address.
    /// </summary>
    public class EmailAddressDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the email address.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the email address type identifier.
        /// </summary>
        public Guid EmailAddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the email address string.
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}