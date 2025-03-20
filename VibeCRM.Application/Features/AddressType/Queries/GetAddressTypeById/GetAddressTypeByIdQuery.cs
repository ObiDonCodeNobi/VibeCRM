using MediatR;
using VibeCRM.Application.Features.AddressType.DTOs;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeById
{
    /// <summary>
    /// Query to retrieve an address type by its unique identifier.
    /// </summary>
    public class GetAddressTypeByIdQuery : IRequest<AddressTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the address type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}