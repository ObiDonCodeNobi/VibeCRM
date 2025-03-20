namespace VibeCRM.Application.Features.Address.DTOs
{
    /// <summary>
    /// Base Data Transfer Object for Address entities.
    /// Contains the essential properties needed for basic operations.
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the address.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the address type.
        /// </summary>
        public Guid AddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the first line of the address.
        /// </summary>
        public string Line1 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the second line of the address.
        /// </summary>
        public string? Line2 { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        public Guid StateId { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string Zip { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether this address is active (not soft-deleted).
        /// When true, the address is active and visible in queries.
        /// When false, the address is considered deleted but remains in the database.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}