using MediatR;

namespace VibeCRM.Application.Features.Address.Commands.DeleteAddress
{
    /// <summary>
    /// Command to delete (soft delete) an address in the system.
    /// This is used in the CQRS pattern as the request object for address deletion.
    /// </summary>
    public class DeleteAddressCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the address to delete.
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAddressCommand"/> class.
        /// </summary>
        /// <param name="addressId">The unique identifier of the address to delete.</param>
        /// <param name="modifiedBy">The identifier of the user performing the deletion.</param>
        public DeleteAddressCommand(Guid addressId, Guid modifiedBy)
        {
            AddressId = addressId;
            ModifiedBy = modifiedBy;
        }
    }
}