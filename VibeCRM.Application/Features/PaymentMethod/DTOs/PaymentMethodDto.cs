namespace VibeCRM.Application.Features.PaymentMethod.DTOs
{
    /// <summary>
    /// Data transfer object for payment method information
    /// </summary>
    public class PaymentMethodDto
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
    }
}