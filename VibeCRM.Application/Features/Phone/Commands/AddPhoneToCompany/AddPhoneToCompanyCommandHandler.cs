using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Commands.AddPhoneToCompany
{
    /// <summary>
    /// Handler for processing AddPhoneToCompanyCommand requests
    /// </summary>
    public class AddPhoneToCompanyCommandHandler : IRequestHandler<AddPhoneToCompanyCommand, bool>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<AddPhoneToCompanyCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPhoneToCompanyCommandHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="companyRepository">The company repository for verifying company existence</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public AddPhoneToCompanyCommandHandler(
            IPhoneRepository phoneRepository,
            ICompanyRepository companyRepository,
            ILogger<AddPhoneToCompanyCommandHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the AddPhoneToCompanyCommand request
        /// </summary>
        /// <param name="request">The command containing the phone and company IDs to associate</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successfully created, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the phone or company does not exist or the association could not be created</exception>
        public async Task<bool> Handle(AddPhoneToCompanyCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating association between phone {PhoneId} and company {CompanyId}", request.PhoneId, request.CompanyId);

                // Verify the phone exists
                if (!await _phoneRepository.ExistsAsync(request.PhoneId, cancellationToken))
                {
                    _logger.LogWarning("Phone with ID {PhoneId} not found", request.PhoneId);
                    throw new InvalidOperationException($"Phone with ID {request.PhoneId} not found");
                }

                // Verify the company exists
                if (!await _companyRepository.ExistsAsync(request.CompanyId, cancellationToken))
                {
                    _logger.LogWarning("Company with ID {CompanyId} not found", request.CompanyId);
                    throw new InvalidOperationException($"Company with ID {request.CompanyId} not found");
                }

                // Check if the association already exists
                if (await _phoneRepository.IsPhoneAssociatedWithCompanyAsync(request.PhoneId, request.CompanyId, cancellationToken))
                {
                    _logger.LogWarning("Phone {PhoneId} is already associated with company {CompanyId}", request.PhoneId, request.CompanyId);
                    return false;
                }

                // Create the association
                bool result = await _phoneRepository.AddPhoneToCompanyAsync(request.PhoneId, request.CompanyId, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully created association between phone {PhoneId} and company {CompanyId}", request.PhoneId, request.CompanyId);
                }
                else
                {
                    _logger.LogWarning("Failed to create association between phone {PhoneId} and company {CompanyId}", request.PhoneId, request.CompanyId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating association between phone {PhoneId} and company {CompanyId}", request.PhoneId, request.CompanyId);
                throw new InvalidOperationException($"Failed to add phone to company: {ex.Message}", ex);
            }
        }
    }
}