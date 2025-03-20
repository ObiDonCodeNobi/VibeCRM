using MediatR;
using VibeCRM.Application.Features.Note.DTOs;

namespace VibeCRM.Application.Features.Note.Queries.GetNotesByPerson
{
    /// <summary>
    /// Query to retrieve all notes associated with a specific person.
    /// This is used in the CQRS pattern as the request object for fetching notes by person.
    /// </summary>
    public class GetNotesByPersonQuery : IRequest<IEnumerable<NoteListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person to retrieve notes for.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNotesByPersonQuery"/> class.
        /// </summary>
        /// <param name="personId">The unique identifier of the person to retrieve notes for.</param>
        public GetNotesByPersonQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}