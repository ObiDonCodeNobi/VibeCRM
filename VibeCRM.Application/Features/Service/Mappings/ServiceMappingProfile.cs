using AutoMapper;
using VibeCRM.Application.Features.Service.Commands.CreateService;
using VibeCRM.Application.Features.Service.Commands.UpdateService;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Shared.DTOs.InvoiceLineItem;
using VibeCRM.Shared.DTOs.QuoteLineItem;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;
using VibeCRM.Shared.DTOs.SalesOrderLineItem_Service;

namespace VibeCRM.Application.Features.Service.Mappings
{
    /// <summary>
    /// Mapping profile for Service entities and related DTOs
    /// </summary>
    public class ServiceMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceMappingProfile"/> class
        /// </summary>
        public ServiceMappingProfile()
        {
            // Map from Service entity to ServiceDto
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Service, ServiceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            // Map from Service entity to ServiceDetailsDto
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Service, ServiceDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ServiceTypeName, opt => opt.MapFrom(src => src.ServiceType != null ? src.ServiceType.Type : string.Empty))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.QuoteLineItems, opt => opt.MapFrom(src => src.QuoteLineItems))
                .ForMember(dest => dest.InvoiceLineItems, opt => opt.MapFrom(src => src.InvoiceLineItems))
                .ForMember(dest => dest.SalesOrderLineItemServices, opt => opt.MapFrom(src => src.SalesOrderLineItemServices))
                .ForMember(dest => dest.SalesOrderLineItems, opt => opt.MapFrom(src => src.SalesOrderLineItems));

            // Map from CreateServiceCommand to Service entity
            CreateMap<CreateServiceCommand, VibeCRM.Domain.Entities.BusinessEntities.Service>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.ServiceType, opt => opt.Ignore())
                .ForMember(dest => dest.QuoteLineItems, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceLineItems, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderLineItemServices, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderLineItems, opt => opt.Ignore());

            // Map from UpdateServiceCommand to Service entity
            CreateMap<UpdateServiceCommand, VibeCRM.Domain.Entities.BusinessEntities.Service>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.ServiceTypeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceType, opt => opt.Ignore())
                .ForMember(dest => dest.QuoteLineItems, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceLineItems, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderLineItemServices, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderLineItems, opt => opt.Ignore());

            // Map from QuoteLineItem entity to QuoteLineItemDto
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.QuoteLineItem, QuoteLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.QuoteId, opt => opt.MapFrom(src => src.QuoteId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
                .ForMember(dest => dest.TaxPercentage, opt => opt.MapFrom(src => src.TaxPercentage))
                .ForMember(dest => dest.LineNumber, opt => opt.MapFrom(src => src.LineNumber))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes));

            // Map from InvoiceLineItem entity to InvoiceLineItemDto
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.InvoiceLineItem, InvoiceLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.SalesOrderLineItemId, opt => opt.MapFrom(src => src.SalesOrderLineItemId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
                .ForMember(dest => dest.TaxPercentage, opt => opt.MapFrom(src => src.TaxPercentage))
                .ForMember(dest => dest.LineNumber, opt => opt.MapFrom(src => src.LineNumber))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes));

            // Map from SalesOrderLineItem entity to SalesOrderLineItemDto
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.SalesOrderLineItem, SalesOrderLineItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SalesOrderId, opt => opt.MapFrom(src => src.SalesOrderId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount));

            // Map from SalesOrderLineItem_Service entity to SalesOrderLineItem_ServiceDto
            CreateMap<VibeCRM.Domain.Entities.JunctionEntities.SalesOrderLineItem_Service, SalesOrderLineItem_ServiceDto>()
                .ForMember(dest => dest.SalesOrderLineItemId, opt => opt.MapFrom(src => src.SalesOrderLineItemId))
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate));
        }
    }
}