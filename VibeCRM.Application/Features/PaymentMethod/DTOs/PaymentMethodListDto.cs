namespace VibeCRM.Application.Features.PaymentMethod.DTOs
{
    /// <summary>
    /// Data transfer object for a list of payment methods
    /// </summary>
    public class PaymentMethodListDto
    {
        /// <summary>
        /// Gets or sets the collection of payment methods
        /// </summary>
        public IEnumerable<PaymentMethodDto> PaymentMethods { get; set; } = new List<PaymentMethodDto>();

        /// <summary>
        /// Gets or sets the total count of payment methods
        /// </summary>
        public int TotalCount { get; set; }
    }
}