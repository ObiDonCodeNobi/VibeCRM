namespace VibeCRM.Shared.DTOs.User
{
    /// <summary>
    /// Data transfer object for user list information
    /// </summary>
    public class UserListDto
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
        /// Gets or sets the date and time of the user's last successful login
        /// </summary>
        public DateTime LastLogin { get; set; }
    }
}