using AutoMapper;
using VibeCRM.Application.Features.InvoiceStatus.Commands.CreateInvoiceStatus;
using VibeCRM.Application.Features.InvoiceStatus.Commands.UpdateInvoiceStatus;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between InvoiceStatus entities and DTOs
    /// </summary>
    public class InvoiceStatusMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the InvoiceStatusMappingProfile class
        /// </summary>
        public InvoiceStatusMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.InvoiceStatus, InvoiceStatusDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceStatusId));

            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.InvoiceStatus, InvoiceStatusDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceStatusId));

            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.InvoiceStatus, InvoiceStatusListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceStatusId))
                .ForMember(dest => dest.InvoiceCount, opt => opt.Ignore()); // This will be populated separately

            // Command to Entity mappings
            CreateMap<CreateInvoiceStatusCommand, VibeCRM.Domain.Entities.TypeStatusEntities.InvoiceStatus>()
                .ForMember(dest => dest.InvoiceStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<UpdateInvoiceStatusCommand, VibeCRM.Domain.Entities.TypeStatusEntities.InvoiceStatus>()
                .ForMember(dest => dest.InvoiceStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            // DTO to Command mappings
            CreateMap<InvoiceStatusDto, CreateInvoiceStatusCommand>();
            CreateMap<InvoiceStatusDto, UpdateInvoiceStatusCommand>();
        }
    }
}