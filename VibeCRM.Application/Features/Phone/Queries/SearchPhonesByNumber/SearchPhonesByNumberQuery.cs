using MediatR;
using VibeCRM.Application.Features.Phone.DTOs;

namespace VibeCRM.Application.Features.Phone.Queries.SearchPhonesByNumber
{
    /// <summary>
    /// Query to search for phones by a partial or complete phone number
    /// </summary>
    public class SearchPhonesByNumberQuery : IRequest<IEnumerable<PhoneListDto>>
    {
        /// <summary>
        /// Gets or sets the search term to match against phone numbers
        /// </summary>
        /// <remarks>
        /// This can be a partial or complete phone number. The search will match any part of the phone number.
        /// </remarks>
        public string SearchTerm { get; set; } = string.Empty;
    }
}