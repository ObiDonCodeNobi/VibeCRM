using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Company.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Company.Commands.CreateCompany
{
    /// <summary>
    /// Handler for processing CreateCompanyCommand requests.
    /// Implements the CQRS command handler pattern for creating company entities.
    /// </summary>
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCompanyCommandHandler"/> class.
        /// </summary>
        /// <param name="companyRepository">The company repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public CreateCompanyCommandHandler(
            ICompanyRepository companyRepository,
            IMapper mapper,
            ILogger<CreateCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateCompanyCommand by creating a new company entity in the database.
        /// </summary>
        /// <param name="request">The CreateCompanyCommand containing the company data to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CompanyDto representing the newly created company.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating new company with ID: {CompanyId}", request.Id);

                // Map the command to an entity
                var companyEntity = _mapper.Map<Domain.Entities.BusinessEntities.Company>(request);

                // Set audit fields
                companyEntity.CreatedDate = DateTime.UtcNow;
                companyEntity.ModifiedDate = DateTime.UtcNow;
                companyEntity.Active = true;

                // Save to database
                var createdCompany = await _companyRepository.AddAsync(companyEntity, cancellationToken);

                _logger.LogInformation("Successfully created company with ID: {CompanyId}", createdCompany.CompanyId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<CompanyDto>(createdCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating company with ID {CompanyId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}