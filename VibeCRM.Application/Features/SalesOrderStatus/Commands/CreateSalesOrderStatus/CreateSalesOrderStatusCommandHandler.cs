using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.CreateSalesOrderStatus
{
    /// <summary>
    /// Handler for the CreateSalesOrderStatusCommand.
    /// Creates a new sales order status in the database.
    /// </summary>
    public class CreateSalesOrderStatusCommandHandler : IRequestHandler<CreateSalesOrderStatusCommand, SalesOrderStatusDto>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSalesOrderStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSalesOrderStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateSalesOrderStatusCommandHandler(
            ISalesOrderStatusRepository salesOrderStatusRepository,
            IMapper mapper,
            ILogger<CreateSalesOrderStatusCommandHandler> logger)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateSalesOrderStatusCommand by creating a new sales order status in the database.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created sales order status DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the creation process.</exception>
        public async Task<SalesOrderStatusDto> Handle(CreateSalesOrderStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new sales order status with name: {StatusName}", request.Status);

                // Map command to entity
                var salesOrderStatusEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.SalesOrderStatus>(request);
                
                // Set audit fields
                salesOrderStatusEntity.CreatedDate = DateTime.UtcNow;
                salesOrderStatusEntity.ModifiedDate = DateTime.UtcNow;
                salesOrderStatusEntity.Active = true;

                // Add to repository
                var createdSalesOrderStatus = await _salesOrderStatusRepository.AddAsync(salesOrderStatusEntity, cancellationToken);

                // Map back to DTO
                var salesOrderStatusDto = _mapper.Map<SalesOrderStatusDto>(createdSalesOrderStatus);

                _logger.LogInformation("Successfully created sales order status with ID: {SalesOrderStatusId}", salesOrderStatusDto.Id);

                return salesOrderStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sales order status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
