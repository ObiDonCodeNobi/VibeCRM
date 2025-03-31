using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.SalesOrderStatus.Commands.CreateSalesOrderStatus;
using VibeCRM.Application.Features.SalesOrderStatus.Commands.DeleteSalesOrderStatus;
using VibeCRM.Application.Features.SalesOrderStatus.Commands.UpdateSalesOrderStatus;
using VibeCRM.Application.Features.SalesOrderStatus.Mappings;
using VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusById;
using VibeCRM.Application.Features.SalesOrderStatus.Queries.GetAllSalesOrderStatuses;
using VibeCRM.Application.Features.SalesOrderStatus.Validators;
using VibeCRM.Shared.DTOs.SalesOrderStatus;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering SalesOrderStatus feature services in the dependency injection container.
    /// </summary>
    public static class SalesOrderStatusFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the SalesOrderStatus feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all SalesOrderStatus feature services added.</returns>
        public static IServiceCollection AddSalesOrderStatusFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateSalesOrderStatusCommand>, CreateSalesOrderStatusCommandValidator>();
            services.AddScoped<IValidator<UpdateSalesOrderStatusCommand>, UpdateSalesOrderStatusCommandValidator>();
            services.AddScoped<IValidator<DeleteSalesOrderStatusCommand>, DeleteSalesOrderStatusCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetSalesOrderStatusByIdQuery>, GetSalesOrderStatusByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllSalesOrderStatusesQuery>, GetAllSalesOrderStatusesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<SalesOrderStatusDto>, SalesOrderStatusDtoValidator>();
            services.AddScoped<IValidator<SalesOrderStatusDetailsDto>, SalesOrderStatusDetailsDtoValidator>();
            services.AddScoped<IValidator<SalesOrderStatusListDto>, SalesOrderStatusListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SalesOrderStatusMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetSalesOrderStatusByIdQuery>();
            });
            
            return services;
        }
    }
}
