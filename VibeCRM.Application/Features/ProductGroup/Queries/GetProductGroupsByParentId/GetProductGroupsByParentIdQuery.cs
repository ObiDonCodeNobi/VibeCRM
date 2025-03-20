using MediatR;
using VibeCRM.Application.Features.ProductGroup.DTOs;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupsByParentId
{
    /// <summary>
    /// Query to retrieve all product groups that have a specific parent product group.
    /// This is used in the CQRS pattern as the request object for retrieving child product groups.
    /// </summary>
    public class GetProductGroupsByParentIdQuery : IRequest<IEnumerable<ProductGroupListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the parent product group whose children to retrieve.
        /// </summary>
        public Guid ParentId { get; set; }
    }
}