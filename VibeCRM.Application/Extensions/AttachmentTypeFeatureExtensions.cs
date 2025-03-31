using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.AttachmentType.Commands.CreateAttachmentType;
using VibeCRM.Application.Features.AttachmentType.Commands.DeleteAttachmentType;
using VibeCRM.Application.Features.AttachmentType.Commands.UpdateAttachmentType;
using VibeCRM.Application.Features.AttachmentType.Mappings;
using VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeById;
using VibeCRM.Application.Features.AttachmentType.Queries.GetAllAttachmentTypes;
using VibeCRM.Application.Features.AttachmentType.Validators;
using VibeCRM.Shared.DTOs.AttachmentType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering AttachmentType feature services in the dependency injection container.
    /// </summary>
    public static class AttachmentTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the AttachmentType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all AttachmentType feature services added.</returns>
        public static IServiceCollection AddAttachmentTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateAttachmentTypeCommand>, CreateAttachmentTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateAttachmentTypeCommand>, UpdateAttachmentTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteAttachmentTypeCommand>, DeleteAttachmentTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetAttachmentTypeByIdQuery>, GetAttachmentTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllAttachmentTypesQuery>, GetAllAttachmentTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<AttachmentTypeDto>, AttachmentTypeDtoValidator>();
            services.AddScoped<IValidator<AttachmentTypeDetailsDto>, AttachmentTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<AttachmentTypeListDto>, AttachmentTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AttachmentTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetAttachmentTypeByIdQuery>();
            });

            return services;
        }
    }
}
