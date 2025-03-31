namespace VibeCRM.Shared.DTOs.Team
{
    /// <summary>
    /// Data transfer object for team information
    /// </summary>
    public class TeamDto
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
        /// Gets or sets a value indicating whether the team is active
        /// </summary>
        public bool Active { get; set; } = true;
    }
}