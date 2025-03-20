namespace VibeCRM.Application.Features.Team.DTOs
{
    /// <summary>
    /// Data transfer object for team list information
    /// </summary>
    public class TeamListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the team
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the employee who leads this team
        /// </summary>
        public Guid TeamLeadEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the team
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the team
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of members in the team
        /// </summary>
        public int MemberCount { get; set; }
    }
}