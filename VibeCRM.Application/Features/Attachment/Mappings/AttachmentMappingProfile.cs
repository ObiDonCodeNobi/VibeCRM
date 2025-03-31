using AutoMapper;
using VibeCRM.Application.Features.Attachment.Commands.CreateAttachment;
using VibeCRM.Application.Features.Attachment.Commands.UpdateAttachment;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Mappings
{
    /// <summary>
    /// Mapping profile for Attachment entity and related DTOs.
    /// Defines how entities are converted to DTOs and vice versa.
    /// </summary>
    public class AttachmentMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentMappingProfile"/> class
        /// and configures the entity-to-DTO mappings.
        /// </summary>
        public AttachmentMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Attachment, AttachmentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttachmentId))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.Filename));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Attachment, AttachmentDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttachmentId))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.Filename))
                .ForMember(dest => dest.AttachmentTypeName, opt => opt.MapFrom(src =>
                    src.AttachmentType != null ? src.AttachmentType.Type : string.Empty));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Attachment, AttachmentListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttachmentId))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.Filename))
                .ForMember(dest => dest.AttachmentTypeName, opt => opt.MapFrom(src =>
                    src.AttachmentType != null ? src.AttachmentType.Type : string.Empty));

            // Command to Entity mappings
            CreateMap<CreateAttachmentCommand, VibeCRM.Domain.Entities.BusinessEntities.Attachment>()
                .ForMember(dest => dest.AttachmentId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.AttachmentType, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore())
                .ForMember(dest => dest.Filename, opt => opt.Ignore());

            CreateMap<UpdateAttachmentCommand, VibeCRM.Domain.Entities.BusinessEntities.Attachment>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.AttachmentType, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore())
                .ForMember(dest => dest.Filename, opt => opt.Ignore());
        }
    }
}