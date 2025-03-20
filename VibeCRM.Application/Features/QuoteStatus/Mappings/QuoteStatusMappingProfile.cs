using AutoMapper;
using VibeCRM.Application.Features.QuoteStatus.Commands.CreateQuoteStatus;
using VibeCRM.Application.Features.QuoteStatus.Commands.UpdateQuoteStatus;
using VibeCRM.Application.Features.QuoteStatus.DTOs;

namespace VibeCRM.Application.Features.QuoteStatus.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between QuoteStatus entities and DTOs.
    /// </summary>
    public class QuoteStatusMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteStatusMappingProfile"/> class.
        /// Configures the mapping between QuoteStatus entities and DTOs.
        /// </summary>
        public QuoteStatusMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.QuoteStatus, QuoteStatusDto>();

            CreateMap<Domain.Entities.TypeStatusEntities.QuoteStatus, QuoteStatusListDto>()
                .ForMember(dest => dest.QuoteCount, opt => opt.MapFrom(src => 0)); // This would typically be populated from a repository

            CreateMap<Domain.Entities.TypeStatusEntities.QuoteStatus, QuoteStatusDetailsDto>()
                .ForMember(dest => dest.QuoteCount, opt => opt.MapFrom(src => 0)); // This would typically be populated from a repository

            // Command to Entity mappings
            CreateMap<CreateQuoteStatusCommand, Domain.Entities.TypeStatusEntities.QuoteStatus>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.QuoteStatusId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateQuoteStatusCommand, Domain.Entities.TypeStatusEntities.QuoteStatus>()
                .ForMember(dest => dest.QuoteStatusId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}