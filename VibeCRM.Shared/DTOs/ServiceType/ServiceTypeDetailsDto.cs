namespace VibeCRM.Shared.DTOs.ServiceType
{
    /// <summary>
    /// Data Transfer Object for detailed service type information.
    /// Includes audit information and additional details for detailed views.
    /// </summary>
    public class ServiceTypeDetailsDto
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

        /// <summary>
        /// Gets or sets a value indicating whether the service type is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the service type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the service type.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the service type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the service type.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}