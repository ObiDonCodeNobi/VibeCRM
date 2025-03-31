using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ShipMethod;

namespace VibeCRM.Application.Features.ShipMethod.Commands.UpdateShipMethod
{
    /// <summary>
    /// Handler for the UpdateShipMethodCommand.
    /// Handles the updating of an existing shipping method.
    /// </summary>
    public class UpdateShipMethodCommandHandler : IRequestHandler<UpdateShipMethodCommand, ShipMethodDto>
    {
        private readonly IShipMethodRepository _shipMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateShipMethodCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateShipMethodCommandHandler"/> class.
        /// </summary>
        /// <param name="shipMethodRepository">The ship method repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateShipMethodCommandHandler(
            IShipMethodRepository shipMethodRepository,
            IMapper mapper,
            ILogger<UpdateShipMethodCommandHandler> logger)
        {
            _shipMethodRepository = shipMethodRepository ?? throw new ArgumentNullException(nameof(shipMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateShipMethodCommand by updating an existing shipping method.
        /// </summary>
        /// <param name="request">The command for updating an existing shipping method.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated shipping method DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the update process.</exception>
        public async Task<ShipMethodDto> Handle(UpdateShipMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating shipping method with ID: {ShipMethodId}", request.Id);

                // Check if the shipping method exists
                var existingShipMethod = await _shipMethodRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingShipMethod == null)
                {
                    _logger.LogWarning("Shipping method with ID: {ShipMethodId} not found", request.Id);
                    throw new Exception($"Shipping method with ID: {request.Id} not found");
                }

                // Map command to entity, preserving original values for fields not in the command
                _mapper.Map(request, existingShipMethod);

                // Update audit fields
                existingShipMethod.ModifiedDate = DateTime.UtcNow;
                existingShipMethod.ModifiedBy = Guid.Empty; // This should be replaced with the current user ID in a real application

                // Update in repository
                var updatedShipMethod = await _shipMethodRepository.UpdateAsync(existingShipMethod, cancellationToken);

                // Map to DTO
                var shipMethodDto = _mapper.Map<ShipMethodDto>(updatedShipMethod);

                _logger.LogInformation("Successfully updated shipping method with ID: {ShipMethodId}", shipMethodDto.Id);

                return shipMethodDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating shipping method with ID {ShipMethodId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}