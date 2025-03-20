using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Commands.RemovePhoneFromPerson
{
    /// <summary>
    /// Handler for processing RemovePhoneFromPersonCommand requests
    /// </summary>
    public class RemovePhoneFromPersonCommandHandler : IRequestHandler<RemovePhoneFromPersonCommand, bool>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<RemovePhoneFromPersonCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePhoneFromPersonCommandHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="personRepository">The person repository for verifying person existence</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public RemovePhoneFromPersonCommandHandler(
            IPhoneRepository phoneRepository,
            IPersonRepository personRepository,
            ILogger<RemovePhoneFromPersonCommandHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the RemovePhoneFromPersonCommand request
        /// </summary>
        /// <param name="request">The command containing the phone and person IDs to disassociate</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successfully removed, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the phone or person does not exist or the association could not be removed</exception>
        public async Task<bool> Handle(RemovePhoneFromPersonCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Removing association between phone {PhoneId} and person {PersonId}", request.PhoneId, request.PersonId);

                // Verify the phone exists
                if (!await _phoneRepository.ExistsAsync(request.PhoneId, cancellationToken))
                {
                    _logger.LogWarning("Phone with ID {PhoneId} not found", request.PhoneId);
                    throw new InvalidOperationException($"Phone with ID {request.PhoneId} not found");
                }

                // Verify the person exists
                if (!await _personRepository.ExistsAsync(request.PersonId, cancellationToken))
                {
                    _logger.LogWarning("Person with ID {PersonId} not found", request.PersonId);
                    throw new InvalidOperationException($"Person with ID {request.PersonId} not found");
                }

                // Check if the association exists
                if (!await _phoneRepository.IsPhoneAssociatedWithPersonAsync(request.PhoneId, request.PersonId, cancellationToken))
                {
                    _logger.LogWarning("Phone {PhoneId} is not associated with person {PersonId}", request.PhoneId, request.PersonId);
                    return false;
                }

                // Remove the association (soft delete - sets Active = 0)
                bool result = await _phoneRepository.RemovePhoneFromPersonAsync(request.PhoneId, request.PersonId, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully removed association between phone {PhoneId} and person {PersonId}", request.PhoneId, request.PersonId);
                }
                else
                {
                    _logger.LogWarning("Failed to remove association between phone {PhoneId} and person {PersonId}", request.PhoneId, request.PersonId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing association between phone {PhoneId} and person {PersonId}", request.PhoneId, request.PersonId);
                throw new InvalidOperationException($"Failed to remove phone from person: {ex.Message}", ex);
            }
        }
    }
}