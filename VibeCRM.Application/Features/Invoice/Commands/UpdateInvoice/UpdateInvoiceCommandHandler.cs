using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Invoice.Commands.UpdateInvoice
{
    /// <summary>
    /// Handler for processing the UpdateInvoiceCommand
    /// </summary>
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInvoiceCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInvoiceCommandHandler"/> class
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
        public UpdateInvoiceCommandHandler(
            IInvoiceRepository invoiceRepository,
            IMapper mapper,
            ILogger<UpdateInvoiceCommandHandler> logger)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateInvoiceCommand to update an existing invoice
        /// </summary>
        /// <param name="request">The command containing the updated invoice data</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the invoice was successfully updated; otherwise, false</returns>
        /// <exception cref="InvalidOperationException">Thrown when the invoice update fails</exception>
        public async Task<bool> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating invoice with ID {InvoiceId}", request.Id);

                // Check if the invoice exists
                var existingInvoice = await _invoiceRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingInvoice == null)
                {
                    _logger.LogWarning("Invoice with ID {InvoiceId} not found", request.Id);
                    return false;
                }

                // Update the invoice with the new values
                _mapper.Map(request, existingInvoice);

                // Set modified date if not already set
                if (existingInvoice.ModifiedDate == default)
                {
                    existingInvoice.ModifiedDate = DateTime.UtcNow;
                }

                // Update the invoice in the repository
                await _invoiceRepository.UpdateAsync(existingInvoice, cancellationToken);

                _logger.LogInformation("Successfully updated invoice with ID {InvoiceId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating invoice with ID {InvoiceId}", request.Id);
                throw new InvalidOperationException($"Failed to update invoice: {ex.Message}", ex);
            }
        }
    }
}