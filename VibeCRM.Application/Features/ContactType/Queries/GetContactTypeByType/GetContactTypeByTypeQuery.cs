using MediatR;
using VibeCRM.Application.Features.ContactType.DTOs;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypeByType
{
    /// <summary>
    /// Query to retrieve a contact type by its type name.
    /// </summary>
    public class GetContactTypeByTypeQuery : IRequest<ContactTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the type name of the contact type to retrieve.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}