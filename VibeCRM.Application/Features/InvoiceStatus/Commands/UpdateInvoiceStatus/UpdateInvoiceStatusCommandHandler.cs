using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Commands.UpdateInvoiceStatus
{
    /// <summary>
    /// Handler for the UpdateInvoiceStatusCommand
    /// </summary>
    public class UpdateInvoiceStatusCommandHandler : IRequestHandler<UpdateInvoiceStatusCommand, InvoiceStatusDto>
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInvoiceStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the UpdateInvoiceStatusCommandHandler class
        /// </summary>
        /// <param name="invoiceStatusRepository">The invoice status repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public UpdateInvoiceStatusCommandHandler(
            IInvoiceStatusRepository invoiceStatusRepository,
            IMapper mapper,
            ILogger<UpdateInvoiceStatusCommandHandler> logger)
        {
            _invoiceStatusRepository = invoiceStatusRepository ?? throw new ArgumentNullException(nameof(invoiceStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateInvoiceStatusCommand
        /// </summary>
        /// <param name="request">The command to update an invoice status</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated invoice status DTO</returns>
        /// <exception cref="ApplicationException">Thrown when the invoice status could not be updated</exception>
        public async Task<InvoiceStatusDto> Handle(UpdateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating invoice status with ID: {Id}", request.Id);

                // Check if invoice status exists
                var existingInvoiceStatus = await _invoiceStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingInvoiceStatus == null)
                {
                    _logger.LogWarning("Invoice status with ID {Id} not found", request.Id);
                    throw new ApplicationException($"Invoice status with ID {request.Id} not found");
                }

                // Map command to entity, preserving original values for fields not included in the update
                var invoiceStatus = _mapper.Map<UpdateInvoiceStatusCommand, VibeCRM.Domain.Entities.TypeStatusEntities.InvoiceStatus>(request, existingInvoiceStatus);

                // Update in repository
                var updatedInvoiceStatus = await _invoiceStatusRepository.UpdateAsync(invoiceStatus, cancellationToken);

                // Map back to DTO
                return _mapper.Map<InvoiceStatusDto>(updatedInvoiceStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating invoice status: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Error updating invoice status: {ex.Message}", ex);
            }
        }
    }
}
