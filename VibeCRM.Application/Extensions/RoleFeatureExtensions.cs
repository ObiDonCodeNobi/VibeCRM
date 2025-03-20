using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Role.Commands.CreateRole;
using VibeCRM.Application.Features.Role.Commands.DeleteRole;
using VibeCRM.Application.Features.Role.Commands.UpdateRole;
using VibeCRM.Application.Features.Role.DTOs;
using VibeCRM.Application.Features.Role.Mappings;
using VibeCRM.Application.Features.Role.Queries.GetAllRoles;
using VibeCRM.Application.Features.Role.Queries.GetRoleById;
using VibeCRM.Application.Features.Role.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Role feature services in the dependency injection container.
    /// </summary>
    public static class RoleFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Role feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Role feature services added.</returns>
        public static IServiceCollection AddRoleFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateRoleCommand>, CreateRoleCommandValidator>();
            services.AddScoped<IValidator<UpdateRoleCommand>, UpdateRoleCommandValidator>();
            services.AddScoped<IValidator<DeleteRoleCommand>, DeleteRoleCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetRoleByIdQuery>, GetRoleByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllRolesQuery>, GetAllRolesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<RoleDto>, RoleDtoValidator>();
            services.AddScoped<IValidator<RoleDetailsDto>, RoleDetailsDtoValidator>();
            services.AddScoped<IValidator<RoleListDto>, RoleListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RoleMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssemblyContaining<GetRoleByIdQuery>();
            });

            return services;
        }
    }
}
