using MediatR;

namespace VibeCRM.Application.Features.User.Commands.DeleteUser
{
    /// <summary>
    /// Command for deleting a user
    /// </summary>
    public class DeleteUserCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is performing the delete operation
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommand"/> class
        /// </summary>
        public DeleteUserCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommand"/> class with specified parameters
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete</param>
        /// <param name="modifiedBy">The identifier of the user who is performing the delete operation</param>
        public DeleteUserCommand(Guid id, Guid modifiedBy)
        {
            Id = id;
            ModifiedBy = modifiedBy;
        }
    }
}