using MediatR;
using VibeCRM.Shared.DTOs.ProductType;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeById
{
    /// <summary>
    /// Query for retrieving a product type by its ID.
    /// </summary>
    public class GetProductTypeByIdQuery : IRequest<ProductTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}