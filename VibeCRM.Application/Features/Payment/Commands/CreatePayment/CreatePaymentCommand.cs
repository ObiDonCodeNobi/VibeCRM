using MediatR;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Commands.CreatePayment
{
    /// <summary>
    /// Command for creating a new payment in the system.
    /// This command encapsulates all data needed to create a payment record.
    /// </summary>
    public class CreatePaymentCommand : IRequest<PaymentDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment.
        /// If not provided, a new GUID will be generated.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the invoice that this payment is associated with, if applicable.
        /// </summary>
        public Guid? InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the payment type identifier indicating how the payment was made (e.g., credit card, cash, check).
        /// </summary>
        public Guid PaymentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the payment method type identifier (e.g., Visa, Mastercard, PayPal).
        /// </summary>
        public Guid? PaymentMethodTypeId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier of the payment.
        /// </summary>
        public Guid PaymentStatusId { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment was made.
        /// </summary>
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the total amount of the payment.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the external reference number for the payment, such as a check number or transaction ID.
        /// </summary>
        public string ReferenceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets additional notes or comments about the payment.
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user who created the payment.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the payment.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}