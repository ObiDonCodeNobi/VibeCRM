using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ServiceType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ServiceType.Commands.UpdateServiceType
{
    /// <summary>
    /// Handler for the UpdateServiceTypeCommand.
    /// Updates an existing service type in the database.
    /// </summary>
    public class UpdateServiceTypeCommandHandler : IRequestHandler<UpdateServiceTypeCommand, ServiceTypeDto>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateServiceTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateServiceTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateServiceTypeCommandHandler(
            IServiceTypeRepository serviceTypeRepository,
            IMapper mapper,
            ILogger<UpdateServiceTypeCommandHandler> logger)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new ArgumentNullException(nameof(serviceTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateServiceTypeCommand by updating an existing service type.
        /// </summary>
        /// <param name="request">The command containing the service type details to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated service type DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the service type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the update process.</exception>
        public async Task<ServiceTypeDto> Handle(UpdateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating service type with ID: {ServiceTypeId}", request.Id);

                // Check if service type exists
                var existingServiceType = await _serviceTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingServiceType == null)
                {
                    _logger.LogError("Service type with ID: {ServiceTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Service type with ID: {request.Id} not found");
                }

                // Map command to entity, preserving original values not in the command
                var serviceTypeEntity = _mapper.Map<UpdateServiceTypeCommand, Domain.Entities.TypeStatusEntities.ServiceType>(request, existingServiceType);

                // Update audit fields
                serviceTypeEntity.ModifiedDate = DateTime.UtcNow;
                serviceTypeEntity.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID

                // Update in repository
                var updatedServiceType = await _serviceTypeRepository.UpdateAsync(serviceTypeEntity, cancellationToken);

                // Map to DTO
                var serviceTypeDto = _mapper.Map<ServiceTypeDto>(updatedServiceType);

                _logger.LogInformation("Successfully updated service type with ID: {ServiceTypeId}", serviceTypeDto.Id);

                return serviceTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
