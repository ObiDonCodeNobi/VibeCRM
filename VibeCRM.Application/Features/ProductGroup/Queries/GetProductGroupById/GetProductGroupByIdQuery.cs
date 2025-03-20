using MediatR;
using VibeCRM.Application.Features.ProductGroup.DTOs;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupById
{
    /// <summary>
    /// Query to retrieve a specific product group by its unique identifier.
    /// This is used in the CQRS pattern as the request object for retrieving a single product group.
    /// </summary>
    public class GetProductGroupByIdQuery : IRequest<ProductGroupDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product group to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}