using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Commands.CreatePersonType
{
    /// <summary>
    /// Handler for the CreatePersonTypeCommand.
    /// Processes requests to create new person types in the system.
    /// </summary>
    public class CreatePersonTypeCommandHandler : IRequestHandler<CreatePersonTypeCommand, Guid>
    {
        private readonly IPersonTypeRepository _personTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePersonTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the CreatePersonTypeCommandHandler class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public CreatePersonTypeCommandHandler(
            IPersonTypeRepository personTypeRepository,
            IMapper mapper,
            ILogger<CreatePersonTypeCommandHandler> logger)
        {
            _personTypeRepository = personTypeRepository ?? throw new ArgumentNullException(nameof(personTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePersonTypeCommand by creating a new person type in the repository.
        /// </summary>
        /// <param name="request">The command containing the person type details to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The ID of the newly created person type.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the person type could not be created.</exception>
        public async Task<Guid> Handle(CreatePersonTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new person type: {Type}", request.Type);

                // Map command to entity
                var personTypeEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.PersonType>(request);

                // Add to repository
                var result = await _personTypeRepository.AddAsync(personTypeEntity, cancellationToken);

                _logger.LogInformation("Successfully created person type with ID: {PersonTypeId}", result.Id);

                return result.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating person type: {Type}. Error: {ErrorMessage}", 
                    request.Type, ex.Message);
                throw new InvalidOperationException($"Failed to create person type: {ex.Message}", ex);
            }
        }
    }
}
