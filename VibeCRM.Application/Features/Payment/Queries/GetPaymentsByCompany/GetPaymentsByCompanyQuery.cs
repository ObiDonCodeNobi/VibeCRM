using MediatR;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByCompany
{
    /// <summary>
    /// Query to retrieve all payments associated with a specific company.
    /// This is used in the CQRS pattern as the request object for fetching payments by company.
    /// </summary>
    public class GetPaymentsByCompanyQuery : IRequest<IEnumerable<PaymentListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company to retrieve payments for.
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByCompanyQuery"/> class.
        /// </summary>
        public GetPaymentsByCompanyQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByCompanyQuery"/> class with a specified company ID.
        /// </summary>
        /// <param name="companyId">The unique identifier of the company to retrieve payments for.</param>
        public GetPaymentsByCompanyQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}