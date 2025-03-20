namespace VibeCRM.Application.Features.Role.DTOs
{
    /// <summary>
    /// Data Transfer Object for listing Role information
    /// </summary>
    public class RoleListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the role
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the role name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the role was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the role was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}