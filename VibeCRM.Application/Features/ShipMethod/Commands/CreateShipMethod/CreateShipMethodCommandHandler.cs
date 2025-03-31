using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ShipMethod;

namespace VibeCRM.Application.Features.ShipMethod.Commands.CreateShipMethod
{
    /// <summary>
    /// Handler for the CreateShipMethodCommand.
    /// Handles the creation of a new shipping method.
    /// </summary>
    public class CreateShipMethodCommandHandler : IRequestHandler<CreateShipMethodCommand, ShipMethodDto>
    {
        private readonly IShipMethodRepository _shipMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateShipMethodCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateShipMethodCommandHandler"/> class.
        /// </summary>
        /// <param name="shipMethodRepository">The ship method repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateShipMethodCommandHandler(
            IShipMethodRepository shipMethodRepository,
            IMapper mapper,
            ILogger<CreateShipMethodCommandHandler> logger)
        {
            _shipMethodRepository = shipMethodRepository ?? throw new ArgumentNullException(nameof(shipMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateShipMethodCommand by creating a new shipping method.
        /// </summary>
        /// <param name="request">The command for creating a new shipping method.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created shipping method DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the creation process.</exception>
        public async Task<ShipMethodDto> Handle(CreateShipMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new shipping method with method name: {MethodName}", request.Method);

                // Map command to entity
                var shipMethod = _mapper.Map<Domain.Entities.TypeStatusEntities.ShipMethod>(request);

                // Set audit fields
                shipMethod.CreatedDate = DateTime.UtcNow;
                shipMethod.CreatedBy = Guid.Empty; // This should be replaced with the current user ID in a real application
                shipMethod.ModifiedDate = shipMethod.CreatedDate;
                shipMethod.ModifiedBy = shipMethod.CreatedBy;
                shipMethod.Active = true;

                // Add to repository
                var createdShipMethod = await _shipMethodRepository.AddAsync(shipMethod, cancellationToken);

                // Map to DTO
                var shipMethodDto = _mapper.Map<ShipMethodDto>(createdShipMethod);

                _logger.LogInformation("Successfully created shipping method with ID: {ShipMethodId}", shipMethodDto.Id);

                return shipMethodDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating shipping method: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}