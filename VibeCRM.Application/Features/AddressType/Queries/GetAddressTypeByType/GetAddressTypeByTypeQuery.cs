using MediatR;
using VibeCRM.Shared.DTOs.AddressType;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeByType
{
    /// <summary>
    /// Query to retrieve an address type by its type name.
    /// </summary>
    public class GetAddressTypeByTypeQuery : IRequest<AddressTypeDto>
    {
        /// <summary>
        /// Gets or sets the type name of the address type to retrieve.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}