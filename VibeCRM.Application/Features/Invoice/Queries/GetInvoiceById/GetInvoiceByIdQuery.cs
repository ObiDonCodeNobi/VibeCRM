using MediatR;
using VibeCRM.Shared.DTOs.Invoice;

namespace VibeCRM.Application.Features.Invoice.Queries.GetInvoiceById
{
    /// <summary>
    /// Query for retrieving an invoice by its unique identifier
    /// </summary>
    public class GetInvoiceByIdQuery : IRequest<InvoiceDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the invoice to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}