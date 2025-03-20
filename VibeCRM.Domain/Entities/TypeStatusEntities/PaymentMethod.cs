using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a payment method in the system, such as Credit Card, Cash, Check, etc.
    /// </summary>
    public class PaymentMethod : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethod"/> class.
        /// </summary>
        public PaymentMethod()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the payment method identifier that directly maps to the PaymentMethodId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid PaymentMethodId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the name of the payment method (e.g., "Credit Card", "Cash", "Check").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the payment method.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting payment methods in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}