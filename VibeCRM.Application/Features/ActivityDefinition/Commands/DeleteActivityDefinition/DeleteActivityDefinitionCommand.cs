using MediatR;

namespace VibeCRM.Application.Features.ActivityDefinition.Commands.DeleteActivityDefinition
{
    /// <summary>
    /// Command for soft-deleting an existing activity definition.
    /// Implements the CQRS command pattern for deleting activity definition entities.
    /// </summary>
    public class DeleteActivityDefinitionCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity definition to delete.
        /// </summary>
        public Guid ActivityDefinitionId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who is performing the delete operation.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}