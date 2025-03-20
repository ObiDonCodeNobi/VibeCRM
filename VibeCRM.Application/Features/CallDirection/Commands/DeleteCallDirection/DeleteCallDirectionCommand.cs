using MediatR;

namespace VibeCRM.Application.Features.CallDirection.Commands.DeleteCallDirection
{
    /// <summary>
    /// Command for soft deleting an existing call direction by setting its Active property to false.
    /// </summary>
    public class DeleteCallDirectionCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the call direction to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is deleting the call direction.
        /// This will be stored as ModifiedBy in the database.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}