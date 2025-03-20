using System;
using MediatR;
using VibeCRM.Application.Features.NoteType.DTOs;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeById
{
    /// <summary>
    /// Query to retrieve a note type by its unique identifier
    /// </summary>
    public class GetNoteTypeByIdQuery : IRequest<NoteTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the note type to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
