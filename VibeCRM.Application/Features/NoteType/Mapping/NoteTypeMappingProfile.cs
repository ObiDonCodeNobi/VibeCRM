using AutoMapper;
using VibeCRM.Application.Features.NoteType.Commands.CreateNoteType;
using VibeCRM.Application.Features.NoteType.Commands.UpdateNoteType;
using VibeCRM.Application.Features.NoteType.DTOs;

namespace VibeCRM.Application.Features.NoteType.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between NoteType entities and DTOs
    /// </summary>
    public class NoteTypeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the NoteTypeMappingProfile class with mapping configurations
        /// </summary>
        public NoteTypeMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.TypeStatusEntities.NoteType, NoteTypeDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.NoteType, NoteTypeDetailsDto>();
            CreateMap<Domain.Entities.TypeStatusEntities.NoteType, NoteTypeListDto>();

            // Command to Entity mappings
            CreateMap<CreateNoteTypeCommand, Domain.Entities.TypeStatusEntities.NoteType>();
            CreateMap<UpdateNoteTypeCommand, Domain.Entities.TypeStatusEntities.NoteType>();

            // DTO to Entity mappings
            CreateMap<NoteTypeDto, Domain.Entities.TypeStatusEntities.NoteType>();
        }
    }
}