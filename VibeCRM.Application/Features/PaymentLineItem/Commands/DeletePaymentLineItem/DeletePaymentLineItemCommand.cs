using MediatR;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.DeletePaymentLineItem
{
    /// <summary>
    /// Command for soft-deleting an existing payment line item.
    /// </summary>
    /// <remarks>
    /// This command encapsulates the data needed to soft-delete a payment line item by setting its Active property to false.
    /// </remarks>
    public class DeletePaymentLineItemCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment line item to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user who is performing the deletion.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}