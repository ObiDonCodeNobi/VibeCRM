using MediatR;
using VibeCRM.Shared.DTOs.Note;

namespace VibeCRM.Application.Features.Note.Queries.GetNotesByCompany
{
    /// <summary>
    /// Query to retrieve all notes associated with a specific company.
    /// This is used in the CQRS pattern as the request object for fetching notes by company.
    /// </summary>
    public class GetNotesByCompanyQuery : IRequest<IEnumerable<NoteListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company to retrieve notes for.
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNotesByCompanyQuery"/> class.
        /// </summary>
        /// <param name="companyId">The unique identifier of the company to retrieve notes for.</param>
        public GetNotesByCompanyQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}