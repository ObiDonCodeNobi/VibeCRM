using MediatR;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.DeletePaymentMethod
{
    /// <summary>
    /// Command to delete (soft delete) an existing payment method
    /// </summary>
    public class DeletePaymentMethodCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the ID of the payment method to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is deleting the payment method
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}