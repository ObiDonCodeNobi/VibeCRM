using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.State.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.State.Queries.GetStatesByAbbreviation
{
    /// <summary>
    /// Handler for the GetStatesByAbbreviationQuery.
    /// Retrieves states by their abbreviation or partial abbreviation match.
    /// </summary>
    public class GetStatesByAbbreviationQueryHandler : IRequestHandler<GetStatesByAbbreviationQuery, IEnumerable<StateListDto>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStatesByAbbreviationQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStatesByAbbreviationQueryHandler"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetStatesByAbbreviationQueryHandler(
            IStateRepository stateRepository,
            IMapper mapper,
            ILogger<GetStatesByAbbreviationQueryHandler> logger)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetStatesByAbbreviationQuery by retrieving states that match the provided abbreviation.
        /// </summary>
        /// <param name="request">The query for retrieving states by abbreviation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of state DTOs that match the search criteria.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<StateListDto>> Handle(GetStatesByAbbreviationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving states with abbreviation containing: {StateAbbreviation}", request.Abbreviation);

                // Get states by abbreviation
                var states = await _stateRepository.GetByAbbreviationAsync(request.Abbreviation, cancellationToken);

                // Map to DTOs
                var stateDtos = _mapper.Map<IEnumerable<StateListDto>>(states);

                // Note: In a real application, you might want to populate AddressCount for each state
                // This would typically involve querying a separate repository or using a join in SQL

                _logger.LogInformation("Successfully retrieved states with abbreviation containing: {StateAbbreviation}", request.Abbreviation);

                return stateDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving states with abbreviation containing {StateAbbreviation}: {ErrorMessage}", request.Abbreviation, ex.Message);
                throw;
            }
        }
    }
}
