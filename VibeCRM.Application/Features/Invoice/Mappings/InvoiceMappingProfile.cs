using AutoMapper;
using VibeCRM.Application.Features.Invoice.Commands.CreateInvoice;
using VibeCRM.Application.Features.Invoice.Commands.UpdateInvoice;
using VibeCRM.Shared.DTOs.Invoice;

namespace VibeCRM.Application.Features.Invoice.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Invoice entities and DTOs
    /// </summary>
    public class InvoiceMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceMappingProfile"/> class
        /// with mappings for Invoice entities and DTOs
        /// </summary>
        public InvoiceMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Invoice, InvoiceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceId));

            CreateMap<Domain.Entities.BusinessEntities.Invoice, InvoiceDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceId));

            CreateMap<Domain.Entities.BusinessEntities.Invoice, InvoiceListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceId));

            // Command to Entity mappings
            CreateMap<CreateInvoiceCommand, Domain.Entities.BusinessEntities.Invoice>()
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateInvoiceCommand, Domain.Entities.BusinessEntities.Invoice>()
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}