using MediatR;
using VibeCRM.Shared.DTOs.AddressType;

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