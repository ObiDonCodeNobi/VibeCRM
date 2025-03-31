using MediatR;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetAllPayments
{
    /// <summary>
    /// Query to retrieve all payments with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching payments.
    /// </summary>
    public class GetAllPaymentsQuery : IRequest<List<PaymentListDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPaymentsQuery"/> class.
        /// </summary>
        public GetAllPaymentsQuery()
        {
        }

        /// <summary>
        /// Gets or sets the page number for pagination (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}