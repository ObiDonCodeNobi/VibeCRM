using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Commands.CreateInvoiceStatus
{
    /// <summary>
    /// Handler for the CreateInvoiceStatusCommand
    /// </summary>
    public class CreateInvoiceStatusCommandHandler : IRequestHandler<CreateInvoiceStatusCommand, InvoiceStatusDto>
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateInvoiceStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the CreateInvoiceStatusCommandHandler class
        /// </summary>
        /// <param name="invoiceStatusRepository">The invoice status repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public CreateInvoiceStatusCommandHandler(
            IInvoiceStatusRepository invoiceStatusRepository,
            IMapper mapper,
            ILogger<CreateInvoiceStatusCommandHandler> logger)
        {
            _invoiceStatusRepository = invoiceStatusRepository ?? throw new ArgumentNullException(nameof(invoiceStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateInvoiceStatusCommand
        /// </summary>
        /// <param name="request">The command to create a new invoice status</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The created invoice status DTO</returns>
        /// <exception cref="ApplicationException">Thrown when the invoice status could not be created</exception>
        public async Task<InvoiceStatusDto> Handle(CreateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating invoice status with status: {Status}", request.Status);

                // Generate a new ID if not provided
                if (request.Id == Guid.Empty)
                {
                    request.Id = Guid.NewGuid();
                }

                // Map command to entity
                var invoiceStatus = _mapper.Map<VibeCRM.Domain.Entities.TypeStatusEntities.InvoiceStatus>(request);

                // Save to repository
                var createdInvoiceStatus = await _invoiceStatusRepository.AddAsync(invoiceStatus, cancellationToken);

                // Map back to DTO
                return _mapper.Map<InvoiceStatusDto>(createdInvoiceStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating invoice status: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Error creating invoice status: {ex.Message}", ex);
            }
        }
    }
}
