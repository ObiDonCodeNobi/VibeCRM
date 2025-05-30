using MediatR;
using VibeCRM.Shared.DTOs.PaymentMethod;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodByName
{
    /// <summary>
    /// Query to retrieve payment methods by their name
    /// </summary>
    public class GetPaymentMethodByNameQuery : IRequest<IEnumerable<PaymentMethodDto>>
    {
        /// <summary>
        /// Gets or sets the name of the payment method to search for
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}