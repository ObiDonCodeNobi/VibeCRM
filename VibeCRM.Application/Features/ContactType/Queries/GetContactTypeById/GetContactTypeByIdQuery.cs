using MediatR;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypeById
{
    /// <summary>
    /// Query to retrieve a contact type by its unique identifier.
    /// </summary>
    public class GetContactTypeByIdQuery : IRequest<ContactTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the contact type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}