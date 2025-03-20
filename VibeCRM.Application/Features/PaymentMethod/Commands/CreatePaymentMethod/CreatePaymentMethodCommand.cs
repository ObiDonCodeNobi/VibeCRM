using MediatR;
using VibeCRM.Application.Features.PaymentMethod.DTOs;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.CreatePaymentMethod
{
    /// <summary>
    /// Command to create a new payment method
    /// </summary>
    public class CreatePaymentMethodCommand : IRequest<PaymentMethodDetailsDto>
    {
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
        /// Gets or sets the ID of the user who created the payment method
        /// </summary>
        public Guid CreatedBy { get; set; }
    }
}