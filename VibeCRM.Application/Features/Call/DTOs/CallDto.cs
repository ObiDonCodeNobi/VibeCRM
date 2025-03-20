namespace VibeCRM.Application.Features.Call.DTOs
{
    /// <summary>
    /// Base DTO for transferring call data between layers.
    /// Contains the essential properties of a call.
    /// </summary>
    public class CallDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the call type identifier.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Gets or sets the call status identifier.
        /// </summary>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Gets or sets the call direction identifier.
        /// </summary>
        public Guid DirectionId { get; set; }

        /// <summary>
        /// Gets or sets the description of the call.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the duration of the call in seconds.
        /// </summary>
        public int Duration { get; set; }

        public CallDto()
        { Description = string.Empty; }
    }
}