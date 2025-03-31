namespace VibeCRM.Shared.DTOs.Role
{
    /// <summary>
    /// Data Transfer Object for detailed Role information including audit properties
    /// </summary>
    public class RoleDetailsDto
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
        /// Gets or sets the identifier of the user who created the role
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the role was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the role
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the role was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the role is active
        /// </summary>
        public bool Active { get; set; }
    }
}