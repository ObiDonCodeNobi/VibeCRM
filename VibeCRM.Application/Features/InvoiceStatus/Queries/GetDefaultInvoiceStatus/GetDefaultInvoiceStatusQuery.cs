using MediatR;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetDefaultInvoiceStatus
{
    /// <summary>
    /// Query to retrieve the default invoice status
    /// </summary>
    public class GetDefaultInvoiceStatusQuery : IRequest<InvoiceStatusDto>
    {
        // No parameters needed as this query retrieves the default status
    }
}
