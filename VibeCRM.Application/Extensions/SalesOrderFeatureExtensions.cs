using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.SalesOrder.Commands.CreateSalesOrder;
using VibeCRM.Application.Features.SalesOrder.Commands.DeleteSalesOrder;
using VibeCRM.Application.Features.SalesOrder.Commands.UpdateSalesOrder;
using VibeCRM.Application.Features.SalesOrder.Mappings;
using VibeCRM.Application.Features.SalesOrder.Queries.GetAllSalesOrders;
using VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderById;
using VibeCRM.Application.Features.SalesOrder.Validators;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering SalesOrder feature services in the dependency injection container.
    /// </summary>
    public static class SalesOrderFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the SalesOrder feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all SalesOrder feature services added.</returns>
        public static IServiceCollection AddSalesOrderFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateSalesOrderCommand>, CreateSalesOrderCommandValidator>();
            services.AddScoped<IValidator<UpdateSalesOrderCommand>, UpdateSalesOrderCommandValidator>();
            services.AddScoped<IValidator<DeleteSalesOrderCommand>, DeleteSalesOrderCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetSalesOrderByIdQuery>, GetSalesOrderByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllSalesOrdersQuery>, GetAllSalesOrdersQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<SalesOrderDto>, SalesOrderDtoValidator>();
            services.AddScoped<IValidator<SalesOrderDetailsDto>, SalesOrderDetailsDtoValidator>();
            services.AddScoped<IValidator<SalesOrderListDto>, SalesOrderListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SalesOrderMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetSalesOrderByIdQuery>();
            });

            return services;
        }
    }
}