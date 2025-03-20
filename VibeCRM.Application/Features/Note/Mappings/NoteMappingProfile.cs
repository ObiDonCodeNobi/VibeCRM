using AutoMapper;
using VibeCRM.Application.Features.Note.Commands.CreateNote;
using VibeCRM.Application.Features.Note.Commands.UpdateNote;
using VibeCRM.Application.Features.Note.DTOs;

namespace VibeCRM.Application.Features.Note.Mappings
{
    /// <summary>
    /// Mapping profile for Note entity and related DTOs.
    /// Defines how entities are converted to DTOs and vice versa.
    /// </summary>
    public class NoteMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteMappingProfile"/> class
        /// and configures the entity-to-DTO mappings.
        /// </summary>
        public NoteMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Note, NoteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NoteId));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Note, NoteDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NoteId))
                .ForMember(dest => dest.NoteTypeName, opt => opt.MapFrom(src =>
                    src.NoteType != null ? src.NoteType.Type : string.Empty));

            CreateMap<VibeCRM.Domain.Entities.BusinessEntities.Note, NoteListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NoteId))
                .ForMember(dest => dest.NoteTypeName, opt => opt.MapFrom(src =>
                    src.NoteType != null ? src.NoteType.Type : string.Empty));

            // Command to Entity mappings
            CreateMap<CreateNoteCommand, VibeCRM.Domain.Entities.BusinessEntities.Note>()
                .ForMember(dest => dest.NoteId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.NoteType, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore());

            CreateMap<UpdateNoteCommand, VibeCRM.Domain.Entities.BusinessEntities.Note>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.NoteType, opt => opt.Ignore())
                .ForMember(dest => dest.Companies, opt => opt.Ignore())
                .ForMember(dest => dest.Persons, opt => opt.Ignore());
        }
    }
}