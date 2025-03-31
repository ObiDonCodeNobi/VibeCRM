using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.CallType.Commands.CreateCallType;
using VibeCRM.Application.Features.CallType.Commands.DeleteCallType;
using VibeCRM.Application.Features.CallType.Commands.UpdateCallType;
using VibeCRM.Application.Features.CallType.Mappings;
using VibeCRM.Application.Features.CallType.Queries.GetCallTypeById;
using VibeCRM.Application.Features.CallType.Queries.GetAllCallTypes;
using VibeCRM.Application.Features.CallType.Validators;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering CallType feature services in the dependency injection container.
    /// </summary>
    public static class CallTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the CallType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all CallType feature services added.</returns>
        public static IServiceCollection AddCallTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateCallTypeCommand>, CreateCallTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateCallTypeCommand>, UpdateCallTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteCallTypeCommand>, DeleteCallTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetCallTypeByIdQuery>, GetCallTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllCallTypesQuery>, GetAllCallTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<CallTypeDto>, CallTypeDtoValidator>();
            services.AddScoped<IValidator<CallTypeDetailsDto>, CallTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<CallTypeListDto>, CallTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CallTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetCallTypeByIdQuery>();
            });

            return services;
        }
    }
}
