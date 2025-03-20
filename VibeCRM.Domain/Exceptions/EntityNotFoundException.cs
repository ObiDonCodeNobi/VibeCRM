namespace VibeCRM.Domain.Exceptions;

/// <summary>
/// Exception thrown when an entity is not found in the data store.
/// </summary>
public class EntityNotFoundException : Exception
{
    /// <summary>
    /// Gets the type of entity that was not found.
    /// </summary>
    public string EntityType { get; init; }

    /// <summary>
    /// Gets the ID of the entity that was not found.
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    public EntityNotFoundException() : base("The requested entity was not found.")
    {
        EntityType = "Unknown";
        Id = "Unknown";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public EntityNotFoundException(string message) : base(message)
    {
        EntityType = "Unknown";
        Id = "Unknown";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        EntityType = "Unknown";
        Id = "Unknown";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with an entity type and ID.
    /// </summary>
    /// <param name="entityType">The type of entity that was not found.</param>
    /// <param name="id">The ID of the entity that was not found.</param>
    public EntityNotFoundException(string entityType, object id)
        : base($"Entity of type {entityType} with ID {id} was not found.")
    {
        EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        Id = id?.ToString() ?? throw new ArgumentNullException(nameof(id));
    }
}