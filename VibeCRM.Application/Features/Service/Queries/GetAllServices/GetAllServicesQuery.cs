using MediatR;
using VibeCRM.Application.Features.Service.DTOs;

namespace VibeCRM.Application.Features.Service.Queries.GetAllServices
{
    /// <summary>
    /// Query to retrieve all services with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching services.
    /// </summary>
    public class GetAllServicesQuery : IRequest<List<ServiceListDto>>
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