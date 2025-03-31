using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Team.Commands.CreateTeam;
using VibeCRM.Application.Features.Team.Commands.DeleteTeam;
using VibeCRM.Application.Features.Team.Commands.UpdateTeam;
using VibeCRM.Application.Features.Team.Mappings;
using VibeCRM.Application.Features.Team.Queries.GetAllTeams;
using VibeCRM.Application.Features.Team.Queries.GetTeamById;
using VibeCRM.Application.Features.Team.Validators;
using VibeCRM.Shared.DTOs.Team;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Team feature services in the dependency injection container.
    /// </summary>
    public static class TeamFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Team feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Team feature services added.</returns>
        public static IServiceCollection AddTeamFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateTeamCommand>, CreateTeamCommandValidator>();
            services.AddScoped<IValidator<UpdateTeamCommand>, UpdateTeamCommandValidator>();
            services.AddScoped<IValidator<DeleteTeamCommand>, DeleteTeamCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetTeamByIdQuery>, GetTeamByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllTeamsQuery>, GetAllTeamsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<TeamDto>, TeamDtoValidator>();
            services.AddScoped<IValidator<TeamDetailsDto>, TeamDetailsDtoValidator>();
            services.AddScoped<IValidator<TeamListDto>, TeamListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TeamMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetTeamByIdQuery>();
            });

            return services;
        }
    }
}