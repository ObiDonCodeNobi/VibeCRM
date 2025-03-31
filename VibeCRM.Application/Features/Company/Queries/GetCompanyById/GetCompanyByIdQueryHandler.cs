using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Company;

namespace VibeCRM.Application.Features.Company.Queries.GetCompanyById
{
    /// <summary>
    /// Handler for processing GetCompanyByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific company.
    /// </summary>
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDetailsDto>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCompanyByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCompanyByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="companyRepository">The company repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetCompanyByIdQueryHandler(
            ICompanyRepository companyRepository,
            IMapper mapper,
            ILogger<GetCompanyByIdQueryHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCompanyByIdQuery by retrieving a specific company entity from the database.
        /// </summary>
        /// <param name="request">The GetCompanyByIdQuery containing the company ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CompanyDetailsDto containing the requested company's data, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the company ID is empty.</exception>
        public async Task<CompanyDetailsDto> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(request.Id));

            try
            {
                _logger.LogInformation("Retrieving company with ID: {CompanyId}", request.Id);

                // Get the company from the repository (Active=1 filter is applied in the repository)
                var company = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);

                if (company == null)
                {
                    _logger.LogWarning("Company with ID {CompanyId} not found or is inactive", request.Id);
                    return new CompanyDetailsDto();
                }

                // Map the entity to a DTO
                var companyDto = _mapper.Map<CompanyDetailsDto>(company);

                _logger.LogInformation("Successfully retrieved company with ID: {CompanyId}", request.Id);

                return companyDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving company with ID {CompanyId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}