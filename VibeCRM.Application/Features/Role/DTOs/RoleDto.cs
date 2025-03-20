namespace VibeCRM.Application.Features.Role.DTOs
{
    /// <summary>
    /// Data Transfer Object for Role information
    /// </summary>
    public class RoleDto
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
    }
}