using MediatR;

namespace VibeCRM.Application.Features.Team.Commands.DeleteTeam
{
    /// <summary>
    /// Command for deleting a team
    /// </summary>
    public class DeleteTeamCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the team to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is performing the delete operation
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTeamCommand"/> class
        /// </summary>
        public DeleteTeamCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTeamCommand"/> class with specified parameters
        /// </summary>
        /// <param name="id">The unique identifier of the team to delete</param>
        /// <param name="modifiedBy">The identifier of the user who is performing the delete operation</param>
        public DeleteTeamCommand(Guid id, Guid modifiedBy)
        {
            Id = id;
            ModifiedBy = modifiedBy;
        }
    }
}