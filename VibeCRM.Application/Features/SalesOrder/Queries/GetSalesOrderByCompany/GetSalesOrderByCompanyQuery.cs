using MediatR;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByCompany
{
    /// <summary>
    /// Query for retrieving sales orders associated with a specific company
    /// </summary>
    public class GetSalesOrderByCompanyQuery : IRequest<IEnumerable<SalesOrderListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}