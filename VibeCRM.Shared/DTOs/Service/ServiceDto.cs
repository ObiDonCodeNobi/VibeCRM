namespace VibeCRM.Application.Features.Service.DTOs
{
    /// <summary>
    /// Base Data Transfer Object for Service entities.
    /// Contains the essential properties needed for basic operations.
    /// </summary>
    public class ServiceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the service.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the service type.
        /// </summary>
        public Guid ServiceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the service.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets whether this service is active (not soft-deleted).
        /// When true, the service is active and visible in queries.
        /// When false, the service is considered deleted but remains in the database.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}