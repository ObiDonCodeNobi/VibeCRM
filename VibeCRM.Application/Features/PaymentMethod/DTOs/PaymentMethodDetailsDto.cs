using System;

namespace VibeCRM.Application.Features.PaymentMethod.DTOs
{
    /// <summary>
    /// Detailed data transfer object for payment method information
    /// </summary>
    public class PaymentMethodDetailsDto
    {
        /// <summary>
        /// Gets or sets the payment method identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the payment method
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the payment method
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting payment methods
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment method was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the payment method
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment method was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the payment method
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the payment method is active
        /// </summary>
        public bool Active { get; set; }
    }
}
