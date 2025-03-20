using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Company.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Company.Queries.GetAllCompanies
{
    /// <summary>
    /// Handler for processing GetAllCompaniesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all active companies.
    /// </summary>
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<CompanyListDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCompaniesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCompaniesQueryHandler"/> class.
        /// </summary>
        /// <param name="companyRepository">The company repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAllCompaniesQueryHandler(
            ICompanyRepository companyRepository,
            IMapper mapper,
            ILogger<GetAllCompaniesQueryHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllCompaniesQuery by retrieving all active company entities from the database.
        /// </summary>
        /// <param name="request">The GetAllCompaniesQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of CompanyListDto objects representing all active companies.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<CompanyListDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving all active companies");

                // Get all active companies from the repository (Active=1 filter is applied in the repository)
                var companies = await _companyRepository.GetAllAsync(cancellationToken);

                // Map the entities to DTOs
                var companyDtos = _mapper.Map<IEnumerable<CompanyListDto>>(companies);

                _logger.LogInformation("Successfully retrieved all active companies");

                return companyDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all active companies: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}