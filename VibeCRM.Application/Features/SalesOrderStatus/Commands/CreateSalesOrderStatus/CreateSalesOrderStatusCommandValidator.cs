using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.CreateSalesOrderStatus
{
    /// <summary>
    /// Validator for the CreateSalesOrderStatusCommand class.
    /// Defines validation rules for creating a new sales order status.
    /// </summary>
    public class CreateSalesOrderStatusCommandValidator : AbstractValidator<CreateSalesOrderStatusCommand>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSalesOrderStatusCommandValidator"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        public CreateSalesOrderStatusCommandValidator(ISalesOrderStatusRepository salesOrderStatusRepository)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));

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
        /// Asynchronously validates that the status name is unique.
        /// </summary>
        /// <param name="status">The status name to validate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the status name is unique, otherwise false.</returns>
        private async Task<bool> BeUniqueStatusAsync(string status, CancellationToken cancellationToken)
        {
            var existingStatuses = await _salesOrderStatusRepository.GetByStatusAsync(status, cancellationToken);
            return !existingStatuses.Any();
        }
    }
}
