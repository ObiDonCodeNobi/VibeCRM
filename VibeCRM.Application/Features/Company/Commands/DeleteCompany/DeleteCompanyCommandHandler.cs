using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Company.Commands.DeleteCompany
{
    /// <summary>
    /// Handler for processing DeleteCompanyCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting company entities.
    /// </summary>
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, bool>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCompanyCommandHandler"/> class.
        /// </summary>
        /// <param name="companyRepository">The company repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public DeleteCompanyCommandHandler(
            ICompanyRepository companyRepository,
            ILogger<DeleteCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteCompanyCommand by soft-deleting an existing company entity in the database.
        /// </summary>
        /// <param name="request">The DeleteCompanyCommand containing the company ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the company ID is empty.</exception>
        public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the existing company (Active=1 filter is applied in the repository)
                var existingCompany = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingCompany == null)
                {
                    _logger.LogWarning("Company with ID {CompanyId} not found or is already inactive", request.Id);
                    return false;
                }

                // Update the modified by information before deletion
                existingCompany.ModifiedBy = request.ModifiedBy;
                existingCompany.ModifiedDate = DateTime.UtcNow;

                // First update the entity to save the modified by information
                await _companyRepository.UpdateAsync(existingCompany, cancellationToken);

                // Then soft delete the company by ID (sets Active = 0)
                var result = await _companyRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Soft-deleted company with ID: {CompanyId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete company with ID: {CompanyId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting company with ID {CompanyId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}