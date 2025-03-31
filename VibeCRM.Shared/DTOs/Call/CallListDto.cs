namespace VibeCRM.Shared.DTOs.Call
{
    /// <summary>
    /// List DTO for transferring call data in list views.
    /// Contains a subset of properties optimized for list displays.
    /// </summary>
    public class CallListDto : CallDto
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
        /// Gets or sets the date and time when the call was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        public CallListDto()
        {
            TypeName = string.Empty;
            StatusName = string.Empty;
            DirectionName = string.Empty;
        }
    }
}