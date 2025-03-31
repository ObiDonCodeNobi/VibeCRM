using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Company;

namespace VibeCRM.Application.Features.Company.Commands.UpdateCompany
{
    /// <summary>
    /// Handler for processing UpdateCompanyCommand requests.
    /// Implements the CQRS command handler pattern for updating company entities.
    /// </summary>
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCompanyCommandHandler"/> class.
        /// </summary>
        /// <param name="companyRepository">The company repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public UpdateCompanyCommandHandler(
            ICompanyRepository companyRepository,
            IMapper mapper,
            ILogger<UpdateCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateCompanyCommand by updating an existing company entity in the database.
        /// </summary>
        /// <param name="request">The UpdateCompanyCommand containing the company data to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CompanyDto representing the updated company.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the company ID is empty.</exception>
        public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(request.Id));

            try
            {
                _logger.LogInformation("Updating company with ID: {CompanyId}", request.Id);

                // Get the existing company (Active=1 filter is applied in the repository)
                var existingCompany = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingCompany == null)
                {
                    _logger.LogWarning("Company with ID {CompanyId} not found or is inactive", request.Id);
                    throw new InvalidOperationException($"Company with ID {request.Id} not found or is inactive");
                }

                // Map the updated properties while preserving existing ones
                _mapper.Map(request, existingCompany);

                // Update audit fields
                existingCompany.ModifiedDate = DateTime.UtcNow;

                // Save to database
                var updatedCompany = await _companyRepository.UpdateAsync(existingCompany, cancellationToken);

                _logger.LogInformation("Successfully updated company with ID: {CompanyId}", updatedCompany.CompanyId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<CompanyDto>(updatedCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating company with ID {CompanyId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}