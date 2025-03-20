using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VibeCRM.Application.Features.User.Commands.CreateUser;
using VibeCRM.Application.Features.User.Commands.DeleteUser;
using VibeCRM.Application.Features.User.Commands.UpdateUser;
using VibeCRM.Application.Features.User.DTOs;
using VibeCRM.Application.Features.User.Mappings;
using VibeCRM.Application.Features.User.Queries.GetAllUsers;
using VibeCRM.Application.Features.User.Queries.GetUserByEmail;
using VibeCRM.Application.Features.User.Queries.GetUserById;
using VibeCRM.Application.Features.User.Queries.GetUserByUsername;
using VibeCRM.Application.Features.User.Queries.GetUsersByRoleId;
using VibeCRM.Application.Features.User.Queries.GetUsersByTeamId;
using VibeCRM.Application.Features.User.Validators;

namespace VibeCRM.Application.DependencyInjection
{
    /// <summary>
    /// Extension methods for registering User feature services in the dependency injection container
    /// </summary>
    public static class UserFeatureExtensions
    {
        /// <summary>
        /// Adds User feature services to the dependency injection container
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection with User feature services added</returns>
        public static IServiceCollection AddUserFeature(this IServiceCollection services)
        {
            // Register AutoMapper profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
            }).CreateMapper());

            // Register command handlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register validators for DTOs
            services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
            services.AddScoped<IValidator<UserDetailsDto>, UserDetailsDtoValidator>();
            services.AddScoped<IValidator<UserListDto>, UserListDtoValidator>();

            // Register validators for commands
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();
            services.AddScoped<IValidator<DeleteUserCommand>, DeleteUserCommandValidator>();

            // Register validators for queries
            services.AddScoped<IValidator<GetUserByIdQuery>, GetUserByIdQueryValidator>();
            services.AddScoped<IValidator<GetUserByUsernameQuery>, GetUserByUsernameQueryValidator>();
            services.AddScoped<IValidator<GetUserByEmailQuery>, GetUserByEmailQueryValidator>();
            services.AddScoped<IValidator<GetUsersByTeamIdQuery>, GetUsersByTeamIdQueryValidator>();
            services.AddScoped<IValidator<GetUsersByRoleIdQuery>, GetUsersByRoleIdQueryValidator>();
            services.AddScoped<IValidator<GetAllUsersQuery>, GetAllUsersQueryValidator>();

            return services;
        }
    }
}