using MediatR;

namespace VibeCRM.Application.Features.AddressType.Commands.DeleteAddressType
{
    /// <summary>
    /// Command for deleting (soft delete) an existing address type.
    /// </summary>
    public class DeleteAddressTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the address type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}