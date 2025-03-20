using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrder.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrder.Commands.CreateSalesOrder
{
    /// <summary>
    /// Handler for processing the CreateSalesOrderCommand
    /// </summary>
    public class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, SalesOrderDto>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSalesOrderCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSalesOrderCommandHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public CreateSalesOrderCommandHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<CreateSalesOrderCommandHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateSalesOrderCommand by creating a new sales order in the database
        /// </summary>
        /// <param name="request">The command containing the sales order details to create</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created sales order DTO with its assigned ID</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the sales order could not be created</exception>
        public async Task<SalesOrderDto> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation("Creating new sales order with number {Number}", request.Number);

                // Map command to entity
                var salesOrderEntity = _mapper.Map<Domain.Entities.BusinessEntities.SalesOrder>(request);
                salesOrderEntity.CreatedDate = DateTime.UtcNow;
                salesOrderEntity.Active = true;

                // Create the sales order in the repository
                var createdSalesOrder = await _salesOrderRepository.AddAsync(salesOrderEntity, cancellationToken);

                if (createdSalesOrder == null)
                {
                    throw new InvalidOperationException($"Failed to create sales order with number {request.Number}");
                }

                _logger.LogInformation("Successfully created sales order with ID {SalesOrderId}", createdSalesOrder.SalesOrderId);

                // Map the created entity back to DTO
                return _mapper.Map<SalesOrderDto>(createdSalesOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sales order with number {Number}: {ErrorMessage}", request.Number, ex.Message);
                throw;
            }
        }
    }
}