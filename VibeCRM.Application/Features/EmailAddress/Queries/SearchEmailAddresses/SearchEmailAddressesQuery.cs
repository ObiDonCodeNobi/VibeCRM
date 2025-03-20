using MediatR;
using VibeCRM.Application.Features.EmailAddress.DTOs;

namespace VibeCRM.Application.Features.EmailAddress.Queries.SearchEmailAddresses
{
    /// <summary>
    /// Query for searching email addresses by a search term.
    /// Implements IRequest to return a collection of EmailAddressListDto objects.
    /// </summary>
    public class SearchEmailAddressesQuery : IRequest<IEnumerable<EmailAddressListDto>>
    {
        /// <summary>
        /// Gets or sets the search term to filter email addresses.
        /// </summary>
        public string SearchTerm { get; set; } = string.Empty;
    }
}