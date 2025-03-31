using MediatR;
using VibeCRM.Shared.DTOs.Quote;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuotesByCompany
{
    /// <summary>
    /// Query for retrieving quotes associated with a specific company
    /// </summary>
    public class GetQuotesByCompanyQuery : IRequest<IEnumerable<QuoteListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company to retrieve quotes for
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}