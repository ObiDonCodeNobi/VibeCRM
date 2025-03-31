using MediatR;
using VibeCRM.Shared.DTOs.Address;

namespace VibeCRM.Application.Features.Address.Queries.GetAllAddresses
{
    /// <summary>
    /// Query to retrieve all addresses from the system with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching addresses.
    /// </summary>
    public class GetAllAddressesQuery : IRequest<List<AddressListDto>>
    {
        /// <summary>
        /// Gets or sets the page number for pagination (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}