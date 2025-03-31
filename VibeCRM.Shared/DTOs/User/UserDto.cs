namespace VibeCRM.Shared.DTOs.User
{
    /// <summary>
    /// Data transfer object for user information
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the person associated with this user
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the login name used for authentication
        /// </summary>
        public string LoginName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password used for authentication
        /// </summary>
        /// <remarks>
        /// This should store a hashed version of the password, never the plaintext password.
        /// </remarks>
        public string LoginPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time of the user's last successful login
        /// </summary>
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is active
        /// </summary>
        public bool Active { get; set; } = true;
    }
}