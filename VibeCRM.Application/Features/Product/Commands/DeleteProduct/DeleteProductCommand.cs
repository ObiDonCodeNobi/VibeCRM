using MediatR;

namespace VibeCRM.Application.Features.Product.Commands.DeleteProduct
{
    /// <summary>
    /// Command to delete (deactivate) an existing product
    /// </summary>
    public class DeleteProductCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product to delete
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the user who deleted the product
        /// </summary>
        public string DeletedBy { get; set; } = string.Empty;
    }
}