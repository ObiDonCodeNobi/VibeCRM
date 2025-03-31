using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VibeCRM.Application.Features.EmailAddressType.Commands.CreateEmailAddressType;
using VibeCRM.Application.Features.EmailAddressType.Commands.DeleteEmailAddressType;
using VibeCRM.Application.Features.EmailAddressType.Commands.UpdateEmailAddressType;
using VibeCRM.Application.Features.EmailAddressType.Mappings;
using VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypeById;
using VibeCRM.Application.Features.EmailAddressType.Queries.GetAllEmailAddressTypes;
using VibeCRM.Application.Features.EmailAddressType.Validators;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering EmailAddressType feature services in the dependency injection container.
    /// </summary>
    public static class EmailAddressTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the EmailAddressType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all EmailAddressType feature services added.</returns>
        public static IServiceCollection AddEmailAddressTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateEmailAddressTypeCommand>, CreateEmailAddressTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateEmailAddressTypeCommand>, UpdateEmailAddressTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteEmailAddressTypeCommand>, DeleteEmailAddressTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetEmailAddressTypeByIdQuery>, GetEmailAddressTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllEmailAddressTypesQuery>, GetAllEmailAddressTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<EmailAddressTypeDto>, EmailAddressTypeDtoValidator>();
            services.AddScoped<IValidator<EmailAddressTypeDetailsDto>, EmailAddressTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<EmailAddressTypeListDto>, EmailAddressTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmailAddressTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetEmailAddressTypeByIdQuery>();
            });

            return services;
        }
    }
}
