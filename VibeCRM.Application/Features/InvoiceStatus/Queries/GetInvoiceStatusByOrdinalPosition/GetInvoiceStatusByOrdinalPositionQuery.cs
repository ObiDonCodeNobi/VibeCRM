using MediatR;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve invoice statuses ordered by their ordinal position
    /// </summary>
    public class GetInvoiceStatusByOrdinalPositionQuery : IRequest<IEnumerable<InvoiceStatusDto>>
    {
        // No parameters needed as this query retrieves all statuses ordered by ordinal position
    }
}