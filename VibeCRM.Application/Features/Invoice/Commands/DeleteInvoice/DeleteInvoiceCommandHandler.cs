using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Invoice.Commands.DeleteInvoice
{
    /// <summary>
    /// Handler for processing the DeleteInvoiceCommand
    /// </summary>
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ILogger<DeleteInvoiceCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteInvoiceCommandHandler"/> class
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
        public DeleteInvoiceCommandHandler(
            IInvoiceRepository invoiceRepository,
            ILogger<DeleteInvoiceCommandHandler> logger)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteInvoiceCommand to soft-delete an existing invoice
        /// </summary>
        /// <param name="request">The command containing the invoice ID to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the invoice was successfully deleted; otherwise, false</returns>
        /// <exception cref="InvalidOperationException">Thrown when the invoice deletion fails</exception>
        public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft-deleting invoice with ID {InvoiceId}", request.Id);

                // Check if the invoice exists
                var existingInvoice = await _invoiceRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingInvoice == null)
                {
                    _logger.LogWarning("Invoice with ID {InvoiceId} not found", request.Id);
                    return false;
                }

                // Update the modified information
                existingInvoice.ModifiedBy = request.ModifiedBy;
                existingInvoice.ModifiedDate = request.ModifiedDate != default ? request.ModifiedDate : DateTime.UtcNow;

                // Set Active to false for soft delete
                existingInvoice.Active = false;

                // Update the invoice in the repository to perform soft delete
                await _invoiceRepository.UpdateAsync(existingInvoice, cancellationToken);

                _logger.LogInformation("Successfully soft-deleted invoice with ID {InvoiceId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft-deleting invoice with ID {InvoiceId}", request.Id);
                throw new InvalidOperationException($"Failed to delete invoice: {ex.Message}", ex);
            }
        }
    }
}