using System;
using MediatR;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.UpdatePaymentMethod
{
    /// <summary>
    /// Command to update an existing payment method
    /// </summary>
    public class UpdatePaymentMethodCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the ID of the payment method to update
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
        /// Gets or sets the ID of the user who modified the payment method
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
