using MediatR;
using VibeCRM.Application.Features.Address.DTOs;

namespace VibeCRM.Application.Features.Address.Commands.CreateAddress
{
    /// <summary>
    /// Command to create a new address in the system.
    /// This is used in the CQRS pattern as the request object for address creation.
    /// </summary>
    public class CreateAddressCommand : IRequest<AddressDetailsDto>
    {
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
        /// Gets or sets the identifier of the user creating this address.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this address.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}