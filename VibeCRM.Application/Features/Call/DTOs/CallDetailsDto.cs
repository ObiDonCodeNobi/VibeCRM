namespace VibeCRM.Application.Features.Call.DTOs
{
    /// <summary>
    /// Detailed DTO for transferring comprehensive call data between layers.
    /// Extends the base CallDto with additional properties for detailed views.
    /// </summary>
    public class CallDetailsDto : CallDto
    {
        /// <summary>
        /// Gets or sets the name of the call type.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the call status.
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the name of the call direction.
        /// </summary>
        public string DirectionName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this call was inbound.
        /// </summary>
        public bool IsInbound { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the call.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the call was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the call.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the call was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        public CallDetailsDto() 
        { 
            TypeName = string.Empty; 
            StatusName = string.Empty; 
            DirectionName = string.Empty; 
        }
    }
}