using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Attachment.Commands.CreateAttachment;
using VibeCRM.Application.Features.Attachment.Commands.DeleteAttachment;
using VibeCRM.Application.Features.Attachment.Commands.UpdateAttachment;
using VibeCRM.Application.Features.Attachment.DTOs;
using VibeCRM.Application.Features.Attachment.Mappings;
using VibeCRM.Application.Features.Attachment.Queries.GetAllAttachments;
using VibeCRM.Application.Features.Attachment.Queries.GetAttachmentById;
using VibeCRM.Application.Features.Attachment.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Attachment feature services in the dependency injection container.
    /// </summary>
    public static class AttachmentFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Attachment feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Attachment feature services added.</returns>
        public static IServiceCollection AddAttachmentFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateAttachmentCommand>, CreateAttachmentCommandValidator>();
            services.AddScoped<IValidator<UpdateAttachmentCommand>, UpdateAttachmentCommandValidator>();
            services.AddScoped<IValidator<DeleteAttachmentCommand>, DeleteAttachmentCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetAttachmentByIdQuery>, GetAttachmentByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllAttachmentsQuery>, GetAllAttachmentsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<AttachmentDto>, AttachmentDtoValidator>();
            services.AddScoped<IValidator<AttachmentDetailsDto>, AttachmentDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AttachmentMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}