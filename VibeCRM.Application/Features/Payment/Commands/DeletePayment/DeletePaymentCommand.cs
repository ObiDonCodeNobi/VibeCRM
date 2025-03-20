using MediatR;

namespace VibeCRM.Application.Features.Payment.Commands.DeletePayment
{
    /// <summary>
    /// Command for soft-deleting a payment in the system.
    /// This command encapsulates the data needed to mark a payment as inactive.
    /// </summary>
    public class DeletePaymentCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is performing the delete operation.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePaymentCommand"/> class.
        /// </summary>
        public DeletePaymentCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePaymentCommand"/> class with specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier of the payment to delete.</param>
        /// <param name="modifiedBy">The identifier of the user who is performing the delete operation.</param>
        public DeletePaymentCommand(Guid id, Guid modifiedBy)
        {
            Id = id;
            ModifiedBy = modifiedBy;
        }
    }
}