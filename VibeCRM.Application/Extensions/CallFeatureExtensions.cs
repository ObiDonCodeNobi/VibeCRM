using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Call.Commands.CreateCall;
using VibeCRM.Application.Features.Call.Commands.DeleteCall;
using VibeCRM.Application.Features.Call.Commands.UpdateCall;
using VibeCRM.Application.Features.Call.DTOs;
using VibeCRM.Application.Features.Call.Mappings;
using VibeCRM.Application.Features.Call.Queries.GetAllCalls;
using VibeCRM.Application.Features.Call.Queries.GetCallById;
using VibeCRM.Application.Features.Call.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Call feature services in the dependency injection container.
    /// </summary>
    public static class CallFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Call feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Call feature services added.</returns>
        public static IServiceCollection AddCallFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateCallCommand>, CreateCallCommandValidator>();
            services.AddScoped<IValidator<UpdateCallCommand>, UpdateCallCommandValidator>();
            services.AddScoped<IValidator<DeleteCallCommand>, DeleteCallCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetCallByIdQuery>, GetCallByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllCallsQuery>, GetAllCallsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<CallDto>, CallDtoValidator>();
            services.AddScoped<IValidator<CallDetailsDto>, CallDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CallMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}