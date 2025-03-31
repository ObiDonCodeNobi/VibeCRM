using MediatR;
using VibeCRM.Shared.DTOs.Call;

namespace VibeCRM.Application.Features.Call.Queries.GetAllCalls
{
    /// <summary>
    /// Query to retrieve all calls with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching calls.
    /// </summary>
    public class GetAllCallsQuery : IRequest<List<CallListDto>>
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