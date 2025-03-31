using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.CallDirection.Commands.CreateCallDirection;
using VibeCRM.Application.Features.CallDirection.Commands.DeleteCallDirection;
using VibeCRM.Application.Features.CallDirection.Commands.UpdateCallDirection;
using VibeCRM.Application.Features.CallDirection.Mappings;
using VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionById;
using VibeCRM.Application.Features.CallDirection.Queries.GetAllCallDirections;
using VibeCRM.Application.Features.CallDirection.Validators;
using VibeCRM.Shared.DTOs.CallDirection;
using MediatR;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering CallDirection feature services in the dependency injection container.
    /// </summary>
    public static class CallDirectionFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the CallDirection feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all CallDirection feature services added.</returns>
        public static IServiceCollection AddCallDirectionFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateCallDirectionCommand>, CreateCallDirectionCommandValidator>();
            services.AddScoped<IValidator<UpdateCallDirectionCommand>, UpdateCallDirectionCommandValidator>();
            services.AddScoped<IValidator<DeleteCallDirectionCommand>, DeleteCallDirectionCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetCallDirectionByIdQuery>, GetCallDirectionByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllCallDirectionsQuery>, GetAllCallDirectionsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<CallDirectionDto>, CallDirectionDtoValidator>();
            services.AddScoped<IValidator<CallDirectionDetailsDto>, CallDirectionDetailsDtoValidator>();
            services.AddScoped<IValidator<CallDirectionListDto>, CallDirectionListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CallDirectionMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetCallDirectionByIdQuery>();
            });

            return services;
        }
    }
}
