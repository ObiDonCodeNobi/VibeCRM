using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a workflow type in the system, such as Sales Process, Customer Onboarding, Support Case, etc.
    /// </summary>
    public class WorkflowType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowType"/> class.
        /// </summary>
        public WorkflowType()
        {
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the workflow type identifier that directly maps to the WorkflowTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid WorkflowTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Sales Process", "Customer Onboarding").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the workflow type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting workflow types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}