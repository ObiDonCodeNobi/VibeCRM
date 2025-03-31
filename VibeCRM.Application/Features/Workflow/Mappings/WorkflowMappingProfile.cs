using AutoMapper;
using VibeCRM.Application.Features.Workflow.Commands.CreateWorkflow;
using VibeCRM.Application.Features.Workflow.Commands.UpdateWorkflow;
using VibeCRM.Shared.DTOs.Workflow;

namespace VibeCRM.Application.Features.Workflow.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping between Workflow entities and DTOs.
    /// Defines the mapping configuration for all workflow-related objects.
    /// </summary>
    public class WorkflowMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowMappingProfile"/> class
        /// and configures the mapping relationships.
        /// </summary>
        public WorkflowMappingProfile()
        {
            // Entity to DTO mappings
            CreateMap<Domain.Entities.BusinessEntities.Workflow, WorkflowDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkflowId));

            CreateMap<Domain.Entities.BusinessEntities.Workflow, WorkflowDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkflowId))
                .ForMember(dest => dest.WorkflowTypeName, opt => opt.Ignore());

            CreateMap<Domain.Entities.BusinessEntities.Workflow, WorkflowListDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkflowId))
                .ForMember(dest => dest.WorkflowTypeName, opt => opt.Ignore());

            // Command to Entity mappings
            CreateMap<CreateWorkflowCommand, Domain.Entities.BusinessEntities.Workflow>()
                .ForMember(dest => dest.WorkflowId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateWorkflowCommand, Domain.Entities.BusinessEntities.Workflow>()
                .ForMember(dest => dest.WorkflowId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}