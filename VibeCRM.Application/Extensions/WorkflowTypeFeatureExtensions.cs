//using AutoMapper;
//using FluentValidation;
//using MediatR;
//using Microsoft.Extensions.DependencyInjection;
//using VibeCRM.Application.Features.WorkflowType.Commands.CreateWorkflowType;
//using VibeCRM.Application.Features.WorkflowType.Commands.DeleteWorkflowType;
//using VibeCRM.Application.Features.WorkflowType.Commands.UpdateWorkflowType;
//using VibeCRM.Application.Features.WorkflowType.Mappings;
//using VibeCRM.Application.Features.WorkflowType.Queries.GetWorkflowTypeById;
//using VibeCRM.Application.Features.WorkflowType.Queries.GetAllWorkflowTypes;
//using VibeCRM.Application.Features.WorkflowType.Validators;
//using VibeCRM.Shared.DTOs.WorkflowType;

//namespace VibeCRM.Application.Extensions
//{
//    /// <summary>
//    /// Provides extension methods for registering WorkflowType feature services in the dependency injection container.
//    /// </summary>
//    public static class WorkflowTypeFeatureExtensions
//    {
//        /// <summary>
//        /// Registers all services required by the WorkflowType feature.
//        /// </summary>
//        /// <param name="services">The service collection to add the services to.</param>
//        /// <returns>The service collection with all WorkflowType feature services added.</returns>
//        public static IServiceCollection AddWorkflowTypeFeature(this IServiceCollection services)
//        {
//            // Register command validators
//            services.AddScoped<IValidator<CreateWorkflowTypeCommand>, CreateWorkflowTypeCommandValidator>();
//            services.AddScoped<IValidator<UpdateWorkflowTypeCommand>, UpdateWorkflowTypeCommandValidator>();
//            services.AddScoped<IValidator<DeleteWorkflowTypeCommand>, DeleteWorkflowTypeCommandValidator>();

//            // Register query validators
//            services.AddScoped<IValidator<GetWorkflowTypeByIdQuery>, GetWorkflowTypeByIdQueryValidator>();
//            services.AddScoped<IValidator<GetAllWorkflowTypesQuery>, GetAllWorkflowTypesQueryValidator>();

//            // Register DTO validators
//            services.AddScoped<IValidator<WorkflowTypeDto>, WorkflowTypeDtoValidator>();
//            services.AddScoped<IValidator<WorkflowTypeDetailsDto>, WorkflowTypeDetailsDtoValidator>();
//            services.AddScoped<IValidator<WorkflowTypeListDto>, WorkflowTypeListDtoValidator>();

//            // Register mapping profiles
//            services.AddSingleton(provider => new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new WorkflowTypeMappingProfile());
//            }).CreateMapper());

//            // Register MediatR handlers
//            services.AddMediatR(cfg =>
//            {
//                cfg.RegisterServicesFromAssemblyContaining<GetWorkflowTypeByIdQuery>();
//            });
            
//            return services;
//        }
//    }
//}
