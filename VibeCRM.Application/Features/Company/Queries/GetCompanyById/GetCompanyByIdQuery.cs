using MediatR;
using VibeCRM.Shared.DTOs.Company;

namespace VibeCRM.Application.Features.Company.Queries.GetCompanyById
{
    /// <summary>
    /// Query for retrieving a specific company by its unique identifier.
    /// Implements the CQRS query pattern for company retrieval.
    /// </summary>
    public class GetCompanyByIdQuery : IRequest<CompanyDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCompanyByIdQuery"/> class.
        /// </summary>
        public GetCompanyByIdQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCompanyByIdQuery"/> class with a specified company ID.
        /// </summary>
        /// <param name="id">The unique identifier of the company to retrieve.</param>
        public GetCompanyByIdQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the company to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}