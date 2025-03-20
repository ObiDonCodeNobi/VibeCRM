using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.DeleteSalesOrderStatus
{
    /// <summary>
    /// Validator for the DeleteSalesOrderStatusCommand class.
    /// Defines validation rules for soft-deleting an existing sales order status.
    /// </summary>
    public class DeleteSalesOrderStatusCommandValidator : AbstractValidator<DeleteSalesOrderStatusCommand>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSalesOrderStatusCommandValidator"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        public DeleteSalesOrderStatusCommandValidator(ISalesOrderStatusRepository salesOrderStatusRepository)
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