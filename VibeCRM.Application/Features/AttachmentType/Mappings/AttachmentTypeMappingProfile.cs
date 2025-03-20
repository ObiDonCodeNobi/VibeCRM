using AutoMapper;
using VibeCRM.Application.Features.AttachmentType.Commands.CreateAttachmentType;
using VibeCRM.Application.Features.AttachmentType.Commands.UpdateAttachmentType;
using VibeCRM.Application.Features.AttachmentType.DTOs;

namespace VibeCRM.Application.Features.AttachmentType.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between AttachmentType entities and DTOs/commands.
    /// </summary>
    public class AttachmentTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentTypeMappingProfile"/> class.
        /// Configures the mappings between AttachmentType entities and DTOs/commands.
        /// </summary>
        public AttachmentTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.AttachmentType, AttachmentTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.AttachmentType, AttachmentTypeListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AttachmentCount, opt => opt.Ignore()); // This will be populated separately

            CreateMap<VibeCRM.Domain.Entities.TypeStatusEntities.AttachmentType, AttachmentTypeDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AttachmentCount, opt => opt.Ignore()); // This will be populated separately

            // Command to Entity mappings
            CreateMap<CreateAttachmentTypeCommand, VibeCRM.Domain.Entities.TypeStatusEntities.AttachmentType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID will be generated in the handler
                .ForMember(dest => dest.Active, opt => opt.Ignore()) // Active will be set in the handler
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // CreatedDate will be set in the handler
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore()) // ModifiedDate will be set in the handler
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore()) // ModifiedBy will be set in the handler
                .ForMember(dest => dest.Attachments, opt => opt.Ignore()); // Attachments will not be set from command

            CreateMap<UpdateAttachmentTypeCommand, VibeCRM.Domain.Entities.TypeStatusEntities.AttachmentType>()
                .ForMember(dest => dest.Active, opt => opt.Ignore()) // Active will not be updated from this command
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // CreatedDate will not be updated
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // CreatedBy will not be updated
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore()) // ModifiedDate will be set in the handler
                .ForMember(dest => dest.Attachments, opt => opt.Ignore()); // Attachments will not be set from command
        }
    }
}