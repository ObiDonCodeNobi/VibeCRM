using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.NoteType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeByType
{
    /// <summary>
    /// Handler for processing GetNoteTypeByTypeQuery requests
    /// </summary>
    public class GetNoteTypeByTypeQueryHandler : IRequestHandler<GetNoteTypeByTypeQuery, NoteTypeDetailsDto>
    {
        private readonly INoteTypeRepository _noteTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetNoteTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetNoteTypeByTypeQueryHandler class
        /// </summary>
        /// <param name="noteTypeRepository">The note type repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetNoteTypeByTypeQueryHandler(
            INoteTypeRepository noteTypeRepository,
            IMapper mapper,
            ILogger<GetNoteTypeByTypeQueryHandler> logger)
        {
            _noteTypeRepository = noteTypeRepository ?? throw new ArgumentNullException(nameof(noteTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetNoteTypeByTypeQuery request
        /// </summary>
        /// <param name="request">The get note type by type query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The note type details DTO</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the note type with the specified type is not found</exception>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of the note type</exception>
        public async Task<NoteTypeDetailsDto> Handle(GetNoteTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving note type with type: {Type}", request.Type);

                var noteType = await _noteTypeRepository.GetByTypeAsync(request.Type, cancellationToken);
                if (noteType == null)
                {
                    _logger.LogWarning("Note type with type: {Type} not found", request.Type);
                    throw new KeyNotFoundException($"Note type with type: {request.Type} not found");
                }

                var noteTypeDto = _mapper.Map<NoteTypeDetailsDto>(noteType);

                _logger.LogInformation("Successfully retrieved note type with type: {Type}", request.Type);
                return noteTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note type with type: {Type}", request.Type);
                throw new ApplicationException($"Error retrieving note type with type: {request.Type}", ex);
            }
        }
    }
}
