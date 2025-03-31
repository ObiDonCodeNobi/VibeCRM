using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.PhoneType.Commands.CreatePhoneType;
using VibeCRM.Application.Features.PhoneType.Commands.DeletePhoneType;
using VibeCRM.Application.Features.PhoneType.Commands.UpdatePhoneType;
using VibeCRM.Application.Features.PhoneType.Mappings;
using VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeById;
using VibeCRM.Application.Features.PhoneType.Queries.GetAllPhoneTypes;
using VibeCRM.Application.Features.PhoneType.Validators;
using VibeCRM.Shared.DTOs.PhoneType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering PhoneType feature services in the dependency injection container.
    /// </summary>
    public static class PhoneTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the PhoneType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all PhoneType feature services added.</returns>
        public static IServiceCollection AddPhoneTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreatePhoneTypeCommand>, CreatePhoneTypeCommandValidator>();
            services.AddScoped<IValidator<UpdatePhoneTypeCommand>, UpdatePhoneTypeCommandValidator>();
            services.AddScoped<IValidator<DeletePhoneTypeCommand>, DeletePhoneTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetPhoneTypeByIdQuery>, GetPhoneTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllPhoneTypesQuery>, GetAllPhoneTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<PhoneTypeDto>, PhoneTypeDtoValidator>();
            services.AddScoped<IValidator<PhoneTypeDetailsDto>, PhoneTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<PhoneTypeListDto>, PhoneTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PhoneTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetPhoneTypeByIdQuery>();
            });
            
            return services;
        }
    }
}
