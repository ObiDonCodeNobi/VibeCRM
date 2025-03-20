using MediatR;
using VibeCRM.Application.Features.AddressType.DTOs;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAllAddressTypes
{
    /// <summary>
    /// Query to retrieve all address types.
    /// </summary>
    public class GetAllAddressTypesQuery : IRequest<IEnumerable<AddressTypeListDto>>
    {
        // No parameters needed for this query
    }
}