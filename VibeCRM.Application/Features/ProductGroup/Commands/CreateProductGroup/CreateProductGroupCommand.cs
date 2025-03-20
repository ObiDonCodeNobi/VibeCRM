using MediatR;
using VibeCRM.Application.Features.ProductGroup.DTOs;

namespace VibeCRM.Application.Features.ProductGroup.Commands.CreateProductGroup
{
    /// <summary>
    /// Command to create a new product group in the system.
    /// This is used in the CQRS pattern as the request object for product group creation.
    /// </summary>
    public class CreateProductGroupCommand : IRequest<ProductGroupDetailsDto>
    {
        /// <summary>
        /// Gets or sets the name of the product group.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product group.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the parent product group identifier, if this is a subgroup.
        /// </summary>
        public Guid? ParentProductGroupId { get; set; }

        /// <summary>
        /// Gets or sets the display order for sorting product groups in listings.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user creating this product group.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this product group.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}