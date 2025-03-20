using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusById
{
    /// <summary>
    /// Validator for the GetSalesOrderStatusByIdQuery class.
    /// Defines validation rules for retrieving a sales order status by ID.
    /// </summary>
    public class GetSalesOrderStatusByIdQueryValidator : AbstractValidator<GetSalesOrderStatusByIdQuery>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderStatusByIdQueryValidator"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        public GetSalesOrderStatusByIdQueryValidator(ISalesOrderStatusRepository salesOrderStatusRepository)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sales order status ID is required.")
                .MustAsync(ExistAsync)
                .WithMessage("Sales order status with the specified ID does not exist.");
        }

        /// <summary>
        /// Asynchronously validates that the sales order status with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the sales order status to validate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the sales order status exists, otherwise false.</returns>
        private async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _salesOrderStatusRepository.ExistsAsync(id, cancellationToken);
        }
    }
}