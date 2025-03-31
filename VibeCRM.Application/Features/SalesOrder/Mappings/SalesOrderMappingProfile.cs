using AutoMapper;
using VibeCRM.Application.Features.SalesOrder.Commands.CreateSalesOrder;
using VibeCRM.Application.Features.SalesOrder.Commands.UpdateSalesOrder;
using VibeCRM.Shared.DTOs.SalesOrder;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrder.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between SalesOrder entities and DTOs
    /// </summary>
    public class SalesOrderMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderMappingProfile"/> class.
        /// </summary>
        public SalesOrderMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.SalesOrder, SalesOrderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderId));

            CreateMap<Domain.Entities.BusinessEntities.SalesOrder, SalesOrderDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.SalesOrderStatusName, opt => opt.MapFrom(src => src.SalesOrderStatus != null ? src.SalesOrderStatus.Status : null))
                .ForMember(dest => dest.ShipMethodName, opt => opt.MapFrom(src => src.ShipMethod != null ? src.ShipMethod.Method : null))
                .ForMember(dest => dest.BillToAddress, opt => opt.MapFrom(src => src.BillToAddress))
                .ForMember(dest => dest.ShipToAddress, opt => opt.MapFrom(src => src.ShipToAddress))
                .ForMember(dest => dest.LineItems, opt => opt.MapFrom(src => src.LineItems));

            CreateMap<Domain.Entities.BusinessEntities.SalesOrder, SalesOrderListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.SalesOrderStatusName, opt => opt.MapFrom(src => src.SalesOrderStatus != null ? src.SalesOrderStatus.Status : null));

            // DTO to Entity mappings
            CreateMap<SalesOrderDto, Domain.Entities.BusinessEntities.SalesOrder>()
                .ForMember(dest => dest.SalesOrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.Quote, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderStatus, opt => opt.Ignore())
                .ForMember(dest => dest.ShipMethod, opt => opt.Ignore())
                .ForMember(dest => dest.BillToAddress, opt => opt.Ignore())
                .ForMember(dest => dest.ShipToAddress, opt => opt.Ignore())
                .ForMember(dest => dest.LineItems, opt => opt.Ignore());

            // Command to Entity mappings
            CreateMap<CreateSalesOrderCommand, Domain.Entities.BusinessEntities.SalesOrder>()
                .ForMember(dest => dest.SalesOrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.Quote, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderStatus, opt => opt.Ignore())
                .ForMember(dest => dest.ShipMethod, opt => opt.Ignore())
                .ForMember(dest => dest.BillToAddress, opt => opt.Ignore())
                .ForMember(dest => dest.ShipToAddress, opt => opt.Ignore())
                .ForMember(dest => dest.LineItems, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateSalesOrderCommand, Domain.Entities.BusinessEntities.SalesOrder>()
                .ForMember(dest => dest.SalesOrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.Quote, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderStatus, opt => opt.Ignore())
                .ForMember(dest => dest.ShipMethod, opt => opt.Ignore())
                .ForMember(dest => dest.BillToAddress, opt => opt.Ignore())
                .ForMember(dest => dest.ShipToAddress, opt => opt.Ignore())
                .ForMember(dest => dest.LineItems, opt => opt.Ignore());

            // Line item mappings
            CreateMap<Domain.Entities.BusinessEntities.SalesOrderLineItem, SalesOrderLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SalesOrderLineItemId));

            CreateMap<SalesOrderLineItemDto, Domain.Entities.BusinessEntities.SalesOrderLineItem>()
                .ForMember(dest => dest.SalesOrderLineItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SalesOrder, opt => opt.Ignore());
        }
    }
}