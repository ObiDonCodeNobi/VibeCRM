using System.Collections.Generic;
using MediatR;
using VibeCRM.Application.Features.NoteType.DTOs;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve note types ordered by their ordinal position
    /// </summary>
    public class GetNoteTypeByOrdinalPositionQuery : IRequest<IEnumerable<NoteTypeDto>>
    {
        // No parameters needed as this query retrieves all note types ordered by ordinal position
    }
}
