using MediatR;

namespace VibeCRM.Application.Features.PaymentStatus.Commands.DeletePaymentStatus
{
    /// <summary>
    /// Command for deleting (soft delete) an existing payment status in the system.
    /// Implements the CQRS command pattern for payment status deletion.
    /// </summary>
    public class DeletePaymentStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment status to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is performing the delete operation.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}