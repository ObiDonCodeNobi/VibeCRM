namespace VibeCRM.Application.Features.ServiceType.DTOs
{
    /// <summary>
    /// Data Transfer Object for service type information in list views.
    /// Includes additional information about service counts.
    /// </summary>
    public class ServiceTypeListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the service type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Consulting", "Implementation", "Training").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the service type with information about
        /// the purpose and features of services in this category.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting service types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of services associated with this service type.
        /// </summary>
        public int ServiceCount { get; set; }
    }
}