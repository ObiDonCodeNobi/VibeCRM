using MediatR;
using VibeCRM.Application.Features.EmailAddress.DTOs;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetAllEmailAddresses
{
    /// <summary>
    /// Query to retrieve all email addresses with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching email addresses.
    /// </summary>
    public class GetAllEmailAddressesQuery : IRequest<List<EmailAddressListDto>>
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