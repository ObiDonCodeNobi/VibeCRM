using MediatR;
using VibeCRM.Shared.DTOs.NoteType;

namespace VibeCRM.Application.Features.NoteType.Queries.GetAllNoteTypes
{
    /// <summary>
    /// Query to retrieve all active note types
    /// </summary>
    public class GetAllNoteTypesQuery : IRequest<IEnumerable<NoteTypeDto>>
    {
        // No parameters needed for this query as it retrieves all note types
    }
}