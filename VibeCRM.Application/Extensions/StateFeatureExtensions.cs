using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.State.Commands.CreateState;
using VibeCRM.Application.Features.State.Commands.DeleteState;
using VibeCRM.Application.Features.State.Commands.UpdateState;
using VibeCRM.Application.Features.State.Mappings;
using VibeCRM.Application.Features.State.Queries.GetStateById;
using VibeCRM.Application.Features.State.Queries.GetAllStates;
using VibeCRM.Application.Features.State.Validators;
using VibeCRM.Shared.DTOs.State;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering State feature services in the dependency injection container.
    /// </summary>
    public static class StateFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the State feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all State feature services added.</returns>
        public static IServiceCollection AddStateFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateStateCommand>, CreateStateCommandValidator>();
            services.AddScoped<IValidator<UpdateStateCommand>, UpdateStateCommandValidator>();
            services.AddScoped<IValidator<DeleteStateCommand>, DeleteStateCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetStateByIdQuery>, GetStateByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllStatesQuery>, GetAllStatesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<StateDto>, StateDtoValidator>();
            services.AddScoped<IValidator<StateDetailsDto>, StateDetailsDtoValidator>();
            services.AddScoped<IValidator<StateListDto>, StateListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StateMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetStateByIdQuery>();
            });
            
            return services;
        }
    }
}
