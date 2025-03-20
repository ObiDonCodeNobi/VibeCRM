using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a team of employees in the system.
    /// </summary>
    public class Team : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Team"/> class.
        /// </summary>
        public Team()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the team identifier that directly maps to the TeamId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid TeamId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the unique identifier of the employee who leads this team.
        /// </summary>
        public Guid TeamLeadEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the team.
        /// </summary>
        public string Description { get; set; }
    }
}