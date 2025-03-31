using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.NoteType.Commands.CreateNoteType;
using VibeCRM.Application.Features.NoteType.Commands.DeleteNoteType;
using VibeCRM.Application.Features.NoteType.Commands.UpdateNoteType;
using VibeCRM.Application.Features.NoteType.Mappings;
using VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeById;
using VibeCRM.Application.Features.NoteType.Queries.GetAllNoteTypes;
using VibeCRM.Application.Features.NoteType.Validators;
using VibeCRM.Shared.DTOs.NoteType;
using MediatR;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering NoteType feature services in the dependency injection container.
    /// </summary>
    public static class NoteTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the NoteType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all NoteType feature services added.</returns>
        public static IServiceCollection AddNoteTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateNoteTypeCommand>, CreateNoteTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateNoteTypeCommand>, UpdateNoteTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteNoteTypeCommand>, DeleteNoteTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetNoteTypeByIdQuery>, GetNoteTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllNoteTypesQuery>, GetAllNoteTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<NoteTypeDto>, NoteTypeDtoValidator>();
            services.AddScoped<IValidator<NoteTypeDetailsDto>, NoteTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<NoteTypeListDto>, NoteTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new NoteTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetNoteTypeByIdQuery>();
            });
            
            return services;
        }
    }
}
