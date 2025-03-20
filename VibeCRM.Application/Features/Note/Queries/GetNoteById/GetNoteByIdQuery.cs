using MediatR;
using VibeCRM.Application.Features.Note.DTOs;

namespace VibeCRM.Application.Features.Note.Queries.GetNoteById
{
    /// <summary>
    /// Query to retrieve a specific note by its unique identifier.
    /// This is used in the CQRS pattern as the request object for fetching a single note.
    /// </summary>
    public class GetNoteByIdQuery : IRequest<NoteDetailsDto?>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the note to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNoteByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the note to retrieve.</param>
        public GetNoteByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}