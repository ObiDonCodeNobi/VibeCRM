using MediatR;

namespace VibeCRM.Application.Features.ProductType.Commands.DeleteProductType
{
    /// <summary>
    /// Command for soft-deleting an existing product type.
    /// </summary>
    public class DeleteProductTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}