using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.UpdateSalesOrderStatus
{
    /// <summary>
    /// Validator for the UpdateSalesOrderStatusCommand class.
    /// Defines validation rules for updating an existing sales order status.
    /// </summary>
    public class UpdateSalesOrderStatusCommandValidator : AbstractValidator<UpdateSalesOrderStatusCommand>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSalesOrderStatusCommandValidator"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        public UpdateSalesOrderStatusCommandValidator(ISalesOrderStatusRepository salesOrderStatusRepository)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sales order status ID is required.")
                .MustAsync(ExistAsync)
                .WithMessage("Sales order status with the specified ID does not exist.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status name is required.")
                .MaximumLength(50)
                .WithMessage("Status name cannot exceed 50 characters.")
                .MustAsync(BeUniqueStatusAsync)
                .WithMessage("Status name must be unique.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
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

        /// <summary>
        /// Asynchronously validates that the status name is unique (except for the current sales order status).
        /// </summary>
        /// <param name="command">The command containing the status name to validate.</param>
        /// <param name="status">The status name to validate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the status name is unique, otherwise false.</returns>
        private async Task<bool> BeUniqueStatusAsync(UpdateSalesOrderStatusCommand command, string status, CancellationToken cancellationToken)
        {
            var existingStatuses = await _salesOrderStatusRepository.GetByStatusAsync(status, cancellationToken);
            return !existingStatuses.Any(x => x.SalesOrderStatusId != command.Id);
        }
    }
}
