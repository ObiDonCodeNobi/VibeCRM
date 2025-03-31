using MediatR;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByDateRange
{
    /// <summary>
    /// Query to retrieve all payments within a specific date range.
    /// This is used in the CQRS pattern as the request object for fetching payments by date range.
    /// </summary>
    public class GetPaymentsByDateRangeQuery : IRequest<IEnumerable<PaymentListDto>>
    {
        /// <summary>
        /// Gets or sets the start date of the range to retrieve payments for.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the range to retrieve payments for.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByDateRangeQuery"/> class.
        /// </summary>
        public GetPaymentsByDateRangeQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByDateRangeQuery"/> class with specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the range to retrieve payments for.</param>
        /// <param name="endDate">The end date of the range to retrieve payments for.</param>
        public GetPaymentsByDateRangeQuery(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}