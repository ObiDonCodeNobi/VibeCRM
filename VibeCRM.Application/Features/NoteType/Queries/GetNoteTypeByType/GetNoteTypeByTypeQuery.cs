using MediatR;
using VibeCRM.Shared.DTOs.NoteType;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeByType
{
    /// <summary>
    /// Query to retrieve a note type by its type name
    /// </summary>
    public class GetNoteTypeByTypeQuery : IRequest<NoteTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the type name to search for
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}