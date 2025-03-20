using MediatR;
using VibeCRM.Application.Features.Company.DTOs;

namespace VibeCRM.Application.Features.Company.Queries.GetAllCompanies
{
    /// <summary>
    /// Query to retrieve all companies with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching companies.
    /// </summary>
    public class GetAllCompaniesQuery : IRequest<List<CompanyListDto>>
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