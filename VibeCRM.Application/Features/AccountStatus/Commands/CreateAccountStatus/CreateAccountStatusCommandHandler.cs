using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AccountStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AccountStatus.Commands.CreateAccountStatus
{
    /// <summary>
    /// Handler for processing CreateAccountStatusCommand requests.
    /// Implements the CQRS command handler pattern for creating account status entities.
    /// </summary>
    public class CreateAccountStatusCommandHandler : IRequestHandler<CreateAccountStatusCommand, AccountStatusDto>
    {
        private readonly IAccountStatusRepository _accountStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAccountStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="accountStatusRepository">The account status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public CreateAccountStatusCommandHandler(
            IAccountStatusRepository accountStatusRepository,
            IMapper mapper,
            ILogger<CreateAccountStatusCommandHandler> logger)
        {
            _accountStatusRepository = accountStatusRepository ?? throw new ArgumentNullException(nameof(accountStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateAccountStatusCommand by creating a new account status entity in the database.
        /// </summary>
        /// <param name="request">The CreateAccountStatusCommand containing the account status data to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AccountStatusDto representing the newly created account status.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<AccountStatusDto> Handle(CreateAccountStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating new account status with ID: {AccountStatusId}", request.Id);

                // Map the command to an entity
                var accountStatusEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.AccountStatus>(request);

                // Set audit fields
                accountStatusEntity.CreatedDate = DateTime.UtcNow;
                accountStatusEntity.ModifiedDate = DateTime.UtcNow;
                accountStatusEntity.Active = true;

                // Save to database
                var createdAccountStatus = await _accountStatusRepository.AddAsync(accountStatusEntity, cancellationToken);

                _logger.LogInformation("Successfully created account status with ID: {AccountStatusId}", createdAccountStatus.AccountStatusId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<AccountStatusDto>(createdAccountStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating account status with ID {AccountStatusId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}