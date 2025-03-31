using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Note;

namespace VibeCRM.Application.Features.Note.Queries.GetNotesByCompany
{
    /// <summary>
    /// Handler for processing GetNotesByCompanyQuery requests.
    /// Implements the CQRS query handler pattern for retrieving notes associated with a specific company.
    /// </summary>
    public class GetNotesByCompanyQueryHandler : IRequestHandler<GetNotesByCompanyQuery, IEnumerable<NoteListDto>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetNotesByCompanyQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNotesByCompanyQueryHandler"/> class.
        /// </summary>
        /// <param name="noteRepository">The note repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetNotesByCompanyQueryHandler(
            INoteRepository noteRepository,
            IMapper mapper,
            ILogger<GetNotesByCompanyQueryHandler> logger)
        {
            _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetNotesByCompanyQuery by retrieving all notes associated with a specific company.
        /// </summary>
        /// <param name="request">The GetNotesByCompanyQuery containing the company ID to retrieve notes for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of NoteListDto objects representing the notes associated with the company.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the company ID is empty.</exception>
        public async Task<IEnumerable<NoteListDto>> Handle(GetNotesByCompanyQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.CompanyId == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(request.CompanyId));

            try
            {
                // Get notes by company ID (Active=1 filter is applied in the repository)
                var notes = await _noteRepository.GetByCompanyAsync(request.CompanyId, cancellationToken);

                _logger.LogInformation("Retrieved {Count} notes for company with ID: {CompanyId}",
                    notes is ICollection<Domain.Entities.BusinessEntities.Note> collection ? collection.Count : "multiple",
                    request.CompanyId);

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<NoteListDto>>(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notes for company with ID {CompanyId}: {ErrorMessage}",
                    request.CompanyId, ex.Message);
                throw;
            }
        }
    }
}