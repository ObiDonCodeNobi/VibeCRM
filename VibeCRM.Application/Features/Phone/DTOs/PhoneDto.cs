namespace VibeCRM.Application.Features.Phone.DTOs
{
    /// <summary>
    /// Data Transfer Object for Phone entity
    /// </summary>
    public class PhoneDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the phone
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the area code
        /// </summary>
        public int AreaCode { get; set; }

        /// <summary>
        /// Gets or sets the prefix
        /// </summary>
        public int Prefix { get; set; }

        /// <summary>
        /// Gets or sets the line number
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the extension
        /// </summary>
        public int? Extension { get; set; }

        /// <summary>
        /// Gets or sets the phone type identifier
        /// </summary>
        public Guid PhoneTypeId { get; set; }

        /// <summary>
        /// Gets or sets the phone type name
        /// </summary>
        public string PhoneTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the formatted phone number
        /// </summary>
        public string FormattedPhoneNumber => $"({AreaCode}) {Prefix}-{LineNumber}{(Extension.HasValue ? $" x{Extension}" : "")}";
    }
}