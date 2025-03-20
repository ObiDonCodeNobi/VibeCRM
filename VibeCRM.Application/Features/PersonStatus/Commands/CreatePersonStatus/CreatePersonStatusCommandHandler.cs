using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonStatus.Commands.CreatePersonStatus
{
    /// <summary>
    /// Handler for processing CreatePersonStatusCommand requests.
    /// Implements the CQRS command handler pattern for creating person status entities.
    /// </summary>
    public class CreatePersonStatusCommandHandler : IRequestHandler<CreatePersonStatusCommand, Guid>
    {
        private readonly IPersonStatusRepository _personStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePersonStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePersonStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="personStatusRepository">The person status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public CreatePersonStatusCommandHandler(
            IPersonStatusRepository personStatusRepository,
            IMapper mapper,
            ILogger<CreatePersonStatusCommandHandler> logger)
        {
            _personStatusRepository = personStatusRepository ?? throw new ArgumentNullException(nameof(personStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePersonStatusCommand by creating a new person status entity.
        /// </summary>
        /// <param name="request">The CreatePersonStatusCommand containing the person status details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The ID of the newly created person status.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<Guid> Handle(CreatePersonStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating new person status with name: {Status}", request.Status);

                // Map command to entity
                var personStatus = _mapper.Map<Domain.Entities.TypeStatusEntities.PersonStatus>(request);

                // Set audit fields if not already set by the mapper
                if (personStatus.CreatedDate == default)
                {
                    personStatus.CreatedDate = DateTime.UtcNow;
                }
                
                if (personStatus.ModifiedDate == default)
                {
                    personStatus.ModifiedDate = personStatus.CreatedDate;
                }
                
                // The CreatedBy and ModifiedBy properties are now handled by the mapper
                personStatus.Active = true;

                // Add to repository
                var result = await _personStatusRepository.AddAsync(personStatus, cancellationToken);

                _logger.LogInformation("Successfully created person status with ID: {PersonStatusId}", result.Id);

                return result.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating person status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
