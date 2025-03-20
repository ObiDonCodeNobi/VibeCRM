using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Phone.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByCompany
{
    /// <summary>
    /// Handler for processing GetPhonesByCompanyQuery requests
    /// </summary>
    public class GetPhonesByCompanyQueryHandler : IRequestHandler<GetPhonesByCompanyQuery, IEnumerable<PhoneListDto>>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPhonesByCompanyQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhonesByCompanyQueryHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="companyRepository">The company repository for verifying company existence</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public GetPhonesByCompanyQueryHandler(
            IPhoneRepository phoneRepository,
            ICompanyRepository companyRepository,
            IMapper mapper,
            ILogger<GetPhonesByCompanyQueryHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPhonesByCompanyQuery request
        /// </summary>
        /// <param name="request">The query containing the company ID to retrieve phones for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phone list DTOs associated with the specified company</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the specified company does not exist</exception>
        public async Task<IEnumerable<PhoneListDto>> Handle(GetPhonesByCompanyQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving phones for company with ID {CompanyId}", request.CompanyId);

                // Verify the company exists
                if (!await _companyRepository.ExistsAsync(request.CompanyId, cancellationToken))
                {
                    _logger.LogWarning("Company with ID {CompanyId} not found", request.CompanyId);
                    throw new InvalidOperationException($"Company with ID {request.CompanyId} not found");
                }

                var phones = await _phoneRepository.GetByCompanyAsync(request.CompanyId, cancellationToken);

                _logger.LogInformation("Successfully retrieved phones for company with ID {CompanyId}", request.CompanyId);

                return _mapper.Map<IEnumerable<PhoneListDto>>(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving phones for company with ID {CompanyId}", request.CompanyId);
                throw;
            }
        }
    }
}