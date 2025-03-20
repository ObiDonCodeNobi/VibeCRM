using MediatR;

namespace VibeCRM.Application.Features.ShipMethod.Commands.DeleteShipMethod
{
    /// <summary>
    /// Command for deleting an existing shipping method.
    /// </summary>
    public class DeleteShipMethodCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the shipping method to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}