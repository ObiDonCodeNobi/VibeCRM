namespace VibeCRM.Application.Features.User.DTOs
{
    /// <summary>
    /// Data transfer object for detailed user information
    /// </summary>
    public class UserDetailsDto : UserDto
    {
        /// <summary>
        /// Gets or sets the name of the user who created this user record
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when this user record was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who last modified this user record
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when this user record was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}