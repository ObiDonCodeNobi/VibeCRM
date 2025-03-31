using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Note.Commands.CreateNote;
using VibeCRM.Application.Features.Note.Commands.DeleteNote;
using VibeCRM.Application.Features.Note.Commands.UpdateNote;
using VibeCRM.Application.Features.Note.Mappings;
using VibeCRM.Application.Features.Note.Queries.GetAllNotes;
using VibeCRM.Application.Features.Note.Queries.GetNoteById;
using VibeCRM.Application.Features.Note.Validators;
using VibeCRM.Shared.DTOs.Note;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Note feature services in the dependency injection container.
    /// </summary>
    public static class NoteFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Note feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Note feature services added.</returns>
        public static IServiceCollection AddNoteFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateNoteCommand>, CreateNoteCommandValidator>();
            services.AddScoped<IValidator<UpdateNoteCommand>, UpdateNoteCommandValidator>();
            services.AddScoped<IValidator<DeleteNoteCommand>, DeleteNoteCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetNoteByIdQuery>, GetNoteByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllNotesQuery>, GetAllNotesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<NoteDto>, NoteDtoValidator>();
            services.AddScoped<IValidator<NoteDetailsDto>, NoteDetailsDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new NoteMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}