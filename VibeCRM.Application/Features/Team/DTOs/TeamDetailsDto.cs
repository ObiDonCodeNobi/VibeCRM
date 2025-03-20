namespace VibeCRM.Application.Features.Team.DTOs
{
    /// <summary>
    /// Data transfer object for detailed team information
    /// </summary>
    public class TeamDetailsDto : TeamDto
    {
        /// <summary>
        /// Gets or sets the name of the user who created the team
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the team was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who last modified the team
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the team was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the number of members in the team
        /// </summary>
        public int MemberCount { get; set; }
    }
}