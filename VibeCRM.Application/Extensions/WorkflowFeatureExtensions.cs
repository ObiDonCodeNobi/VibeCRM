using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Workflow.Commands.CreateWorkflow;
using VibeCRM.Application.Features.Workflow.Commands.DeleteWorkflow;
using VibeCRM.Application.Features.Workflow.Commands.UpdateWorkflow;
using VibeCRM.Application.Features.Workflow.Mappings;
using VibeCRM.Application.Features.Workflow.Queries.GetAllWorkflows;
using VibeCRM.Application.Features.Workflow.Queries.GetWorkflowById;
using VibeCRM.Application.Features.Workflow.Queries.GetWorkflowsByWorkflowType;
using VibeCRM.Application.Features.Workflow.Validators;
using VibeCRM.Shared.DTOs.Workflow;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Workflow feature services in the dependency injection container.
    /// </summary>
    public static class WorkflowFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Workflow feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Workflow feature services added.</returns>
        public static IServiceCollection AddWorkflowFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateWorkflowCommand>, CreateWorkflowCommandValidator>();
            services.AddScoped<IValidator<UpdateWorkflowCommand>, UpdateWorkflowCommandValidator>();
            services.AddScoped<IValidator<DeleteWorkflowCommand>, DeleteWorkflowCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetWorkflowByIdQuery>, GetWorkflowByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllWorkflowsQuery>, GetAllWorkflowsQueryValidator>();
            services.AddScoped<IValidator<GetWorkflowsByWorkflowTypeQuery>, GetWorkflowsByWorkflowTypeQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<WorkflowDto>, WorkflowDtoValidator>();
            services.AddScoped<IValidator<WorkflowDetailsDto>, WorkflowDetailsDtoValidator>();
            services.AddScoped<IValidator<WorkflowListDto>, WorkflowListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WorkflowMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetWorkflowsByWorkflowTypeQuery>();
            });

            return services;
        }
    }
}