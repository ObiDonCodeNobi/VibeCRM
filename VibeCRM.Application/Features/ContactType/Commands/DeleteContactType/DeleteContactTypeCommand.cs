using MediatR;

namespace VibeCRM.Application.Features.ContactType.Commands.DeleteContactType
{
    /// <summary>
    /// Command for deleting (soft delete) an existing contact type.
    /// </summary>
    public class DeleteContactTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the contact type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}