using MediatR;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByPerson
{
    /// <summary>
    /// Query to retrieve all payments associated with a specific person.
    /// This is used in the CQRS pattern as the request object for fetching payments by person.
    /// </summary>
    public class GetPaymentsByPersonQuery : IRequest<IEnumerable<PaymentListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person to retrieve payments for.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByPersonQuery"/> class.
        /// </summary>
        public GetPaymentsByPersonQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByPersonQuery"/> class with a specified person ID.
        /// </summary>
        /// <param name="personId">The unique identifier of the person to retrieve payments for.</param>
        public GetPaymentsByPersonQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}