using MediatR;
using VibeCRM.Shared.DTOs.Address;

namespace VibeCRM.Application.Features.Address.Queries.GetAddressById
{
    /// <summary>
    /// Query to retrieve a specific address by its unique identifier.
    /// This is used in the CQRS pattern as the request object for fetching a single address.
    /// </summary>
    public class GetAddressByIdQuery : IRequest<AddressDetailsDto?>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the address to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAddressByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the address to retrieve.</param>
        public GetAddressByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}