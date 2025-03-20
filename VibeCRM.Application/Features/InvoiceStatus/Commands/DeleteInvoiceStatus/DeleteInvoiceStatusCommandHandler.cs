using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Commands.DeleteInvoiceStatus
{
    /// <summary>
    /// Handler for the DeleteInvoiceStatusCommand
    /// </summary>
    public class DeleteInvoiceStatusCommandHandler : IRequestHandler<DeleteInvoiceStatusCommand, bool>
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;
        private readonly ILogger<DeleteInvoiceStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the DeleteInvoiceStatusCommandHandler class
        /// </summary>
        /// <param name="invoiceStatusRepository">The invoice status repository</param>
        /// <param name="logger">The logger</param>
        public DeleteInvoiceStatusCommandHandler(
            IInvoiceStatusRepository invoiceStatusRepository,
            ILogger<DeleteInvoiceStatusCommandHandler> logger)
        {
            _invoiceStatusRepository = invoiceStatusRepository ?? throw new ArgumentNullException(nameof(invoiceStatusRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteInvoiceStatusCommand
        /// </summary>
        /// <param name="request">The command to delete an invoice status</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the invoice status was successfully deleted (soft deleted); otherwise, false</returns>
        /// <exception cref="ApplicationException">Thrown when the invoice status could not be deleted</exception>
        public async Task<bool> Handle(DeleteInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting (soft delete) invoice status with ID: {Id}", request.Id);

                // Check if invoice status exists
                var exists = await _invoiceStatusRepository.ExistsAsync(request.Id, cancellationToken);
                if (!exists)
                {
                    _logger.LogWarning("Invoice status with ID {Id} not found", request.Id);
                    return false;
                }

                // Perform soft delete by setting Active = false
                await _invoiceStatusRepository.DeleteAsync(request.Id, cancellationToken);

                _logger.LogInformation("Successfully soft deleted invoice status with ID: {Id}", request.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting invoice status: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Error deleting invoice status: {ex.Message}", ex);
            }
        }
    }
}