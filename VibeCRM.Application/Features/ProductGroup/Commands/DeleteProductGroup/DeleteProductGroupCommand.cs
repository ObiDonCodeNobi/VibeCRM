using MediatR;

namespace VibeCRM.Application.Features.ProductGroup.Commands.DeleteProductGroup
{
    /// <summary>
    /// Command to delete (soft delete) an existing product group in the system.
    /// This is used in the CQRS pattern as the request object for product group deletion.
    /// </summary>
    public class DeleteProductGroupCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product group to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}