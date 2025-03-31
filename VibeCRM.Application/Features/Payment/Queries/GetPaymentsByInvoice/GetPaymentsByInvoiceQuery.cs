using MediatR;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByInvoice
{
    /// <summary>
    /// Query to retrieve all payments associated with a specific invoice.
    /// This is used in the CQRS pattern as the request object for fetching payments by invoice.
    /// </summary>
    public class GetPaymentsByInvoiceQuery : IRequest<IEnumerable<PaymentListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the invoice to retrieve payments for.
        /// </summary>
        public Guid InvoiceId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByInvoiceQuery"/> class.
        /// </summary>
        public GetPaymentsByInvoiceQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByInvoiceQuery"/> class with a specified invoice ID.
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice to retrieve payments for.</param>
        public GetPaymentsByInvoiceQuery(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}