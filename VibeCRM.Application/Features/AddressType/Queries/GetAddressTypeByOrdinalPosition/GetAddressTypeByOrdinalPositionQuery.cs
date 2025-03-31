using MediatR;
using VibeCRM.Shared.DTOs.AddressType;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve an address type by its ordinal position.
    /// </summary>
    public class GetAddressTypeByOrdinalPositionQuery : IRequest<AddressTypeDto>
    {
        /// <summary>
        /// Gets or sets the ordinal position of the address type to retrieve.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}