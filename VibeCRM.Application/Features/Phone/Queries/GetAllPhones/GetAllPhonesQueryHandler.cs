using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Queries.GetAllPhones
{
    /// <summary>
    /// Handler for processing GetAllPhonesQuery requests
    /// </summary>
    public class GetAllPhonesQueryHandler : IRequestHandler<GetAllPhonesQuery, IEnumerable<PhoneListDto>>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPhonesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPhonesQueryHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public GetAllPhonesQueryHandler(
            IPhoneRepository phoneRepository,
            IMapper mapper,
            ILogger<GetAllPhonesQueryHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPhonesQuery request
        /// </summary>
        /// <param name="request">The query to retrieve all active phones</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phone list DTOs representing all active phones</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<IEnumerable<PhoneListDto>> Handle(GetAllPhonesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving all active phones");

                var phones = await _phoneRepository.GetAllAsync(cancellationToken);

                _logger.LogInformation("Successfully retrieved all active phones");

                return _mapper.Map<IEnumerable<PhoneListDto>>(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all active phones");
                throw;
            }
        }
    }
}