using MediatR;
using VibeCRM.Shared.DTOs.AddressType;

namespace VibeCRM.Application.Features.AddressType.Queries.GetDefaultAddressType
{
    /// <summary>
    /// Query to retrieve the default address type.
    /// </summary>
    public class GetDefaultAddressTypeQuery : IRequest<AddressTypeDto>
    {
        // No parameters needed for this query
    }
}