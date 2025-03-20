using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ServiceType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ServiceType.Commands.CreateServiceType
{
    /// <summary>
    /// Handler for the CreateServiceTypeCommand.
    /// Creates a new service type in the database.
    /// </summary>
    public class CreateServiceTypeCommandHandler : IRequestHandler<CreateServiceTypeCommand, ServiceTypeDto>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateServiceTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateServiceTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateServiceTypeCommandHandler(
            IServiceTypeRepository serviceTypeRepository,
            IMapper mapper,
            ILogger<CreateServiceTypeCommandHandler> logger)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new ArgumentNullException(nameof(serviceTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateServiceTypeCommand by creating a new service type.
        /// </summary>
        /// <param name="request">The command containing the service type details to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created service type DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the creation process.</exception>
        public async Task<ServiceTypeDto> Handle(CreateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new service type with type: {ServiceType}", request.Type);

                // Map command to entity
                var serviceTypeEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.ServiceType>(request);

                // Set audit fields
                serviceTypeEntity.Id = Guid.NewGuid();
                serviceTypeEntity.CreatedDate = DateTime.UtcNow;
                serviceTypeEntity.CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                serviceTypeEntity.ModifiedDate = DateTime.UtcNow;
                serviceTypeEntity.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                serviceTypeEntity.Active = true;

                // Add to repository
                var createdServiceType = await _serviceTypeRepository.AddAsync(serviceTypeEntity, cancellationToken);

                // Map to DTO
                var serviceTypeDto = _mapper.Map<ServiceTypeDto>(createdServiceType);

                _logger.LogInformation("Successfully created service type with ID: {ServiceTypeId}", serviceTypeDto.Id);

                return serviceTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating service type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
