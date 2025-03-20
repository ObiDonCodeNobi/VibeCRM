using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Invoice.Commands.CreateInvoice
{
    /// <summary>
    /// Handler for processing the CreateInvoiceCommand
    /// </summary>
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Guid>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateInvoiceCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInvoiceCommandHandler"/> class
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
        public CreateInvoiceCommandHandler(
            IInvoiceRepository invoiceRepository,
            IMapper mapper,
            ILogger<CreateInvoiceCommandHandler> logger)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateInvoiceCommand to create a new invoice
        /// </summary>
        /// <param name="request">The command containing the invoice data to create</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The ID of the created invoice</returns>
        /// <exception cref="InvalidOperationException">Thrown when the invoice creation fails</exception>
        public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating invoice with number {InvoiceNumber}", request.Number);

                // Generate a new ID if not provided
                if (request.Id == Guid.Empty)
                {
                    request.Id = Guid.NewGuid();
                }

                // Map command to entity
                var invoice = _mapper.Map<Domain.Entities.BusinessEntities.Invoice>(request);

                // Set audit fields if not already set
                if (invoice.CreatedDate == default)
                {
                    invoice.CreatedDate = DateTime.UtcNow;
                }

                if (invoice.ModifiedDate == default)
                {
                    invoice.ModifiedDate = invoice.CreatedDate;
                }

                if (invoice.ModifiedBy == Guid.Empty)
                {
                    invoice.ModifiedBy = invoice.CreatedBy;
                }

                // Ensure Active is set to true
                invoice.Active = true;

                // Add the invoice to the repository
                var createdInvoice = await _invoiceRepository.AddAsync(invoice, cancellationToken);

                _logger.LogInformation("Successfully created invoice with ID {InvoiceId}", createdInvoice.InvoiceId);

                return createdInvoice.InvoiceId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating invoice with number {InvoiceNumber}", request.Number);
                throw new InvalidOperationException($"Failed to create invoice: {ex.Message}", ex);
            }
        }
    }
}