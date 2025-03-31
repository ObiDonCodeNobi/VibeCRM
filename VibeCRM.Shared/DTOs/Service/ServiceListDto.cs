namespace VibeCRM.Application.Features.Service.DTOs
{
    /// <summary>
    /// List Data Transfer Object for Service entities.
    /// Contains properties needed for displaying services in list views.
    /// </summary>
    public class ServiceListDto
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
        /// Gets or sets the name of the service type.
        /// </summary>
        public string ServiceTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the service.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this service was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this service was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}