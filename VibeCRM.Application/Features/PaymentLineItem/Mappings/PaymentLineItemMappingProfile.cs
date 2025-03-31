using AutoMapper;
using VibeCRM.Application.Features.PaymentLineItem.Commands.CreatePaymentLineItem;
using VibeCRM.Application.Features.PaymentLineItem.Commands.UpdatePaymentLineItem;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between PaymentLineItem entities and DTOs.
    /// </summary>
    /// <remarks>
    /// Defines mappings for various PaymentLineItem-related classes to ensure proper data transformation.
    /// </remarks>
    public class PaymentLineItemMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentLineItemMappingProfile"/> class.
        /// </summary>
        public PaymentLineItemMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.PaymentLineItem, PaymentLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentLineItemId));

            CreateMap<Domain.Entities.BusinessEntities.PaymentLineItem, PaymentLineItemDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentLineItemId));

            CreateMap<Domain.Entities.BusinessEntities.PaymentLineItem, PaymentLineItemListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentLineItemId));

            // Command to Entity mappings
            CreateMap<CreatePaymentLineItemCommand, Domain.Entities.BusinessEntities.PaymentLineItem>()
                .ForMember(dest => dest.PaymentLineItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => System.DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(_ => System.DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(_ => true));

            CreateMap<UpdatePaymentLineItemCommand, Domain.Entities.BusinessEntities.PaymentLineItem>()
                .ForMember(dest => dest.PaymentLineItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(_ => System.DateTime.UtcNow))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}