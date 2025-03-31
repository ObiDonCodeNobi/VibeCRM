using MediatR;
using VibeCRM.Shared.DTOs.EmailAddress;

namespace VibeCRM.Application.Features.EmailAddress.Queries.GetEmailAddressesByType
{
    /// <summary>
    /// Query to retrieve email addresses filtered by type with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching filtered email addresses.
    /// </summary>
    public class GetEmailAddressesByTypeQuery : IRequest<List<EmailAddressListDto>>
    {
        /// <summary>
        /// Gets or sets the email address type ID to filter by.
        /// </summary>
        public Guid EmailAddressTypeId { get; set; }

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