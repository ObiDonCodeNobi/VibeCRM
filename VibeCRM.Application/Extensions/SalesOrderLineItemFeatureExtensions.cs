using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.CreateSalesOrderLineItem;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.DeleteSalesOrderLineItem;
using VibeCRM.Application.Features.SalesOrderLineItem.Commands.UpdateSalesOrderLineItem;
using VibeCRM.Application.Features.SalesOrderLineItem.Mappings;
using VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetAllSalesOrderLineItems;
using VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemById;
using VibeCRM.Application.Features.SalesOrderLineItem.Validators;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering SalesOrderLineItem feature services in the dependency injection container.
    /// </summary>
    public static class SalesOrderLineItemFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the SalesOrderLineItem feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all SalesOrderLineItem feature services added.</returns>
        public static IServiceCollection AddSalesOrderLineItemFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateSalesOrderLineItemCommand>, CreateSalesOrderLineItemCommandValidator>();
            services.AddScoped<IValidator<UpdateSalesOrderLineItemCommand>, UpdateSalesOrderLineItemCommandValidator>();
            services.AddScoped<IValidator<DeleteSalesOrderLineItemCommand>, DeleteSalesOrderLineItemCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetSalesOrderLineItemByIdQuery>, GetSalesOrderLineItemByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllSalesOrderLineItemsQuery>, GetAllSalesOrderLineItemsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<SalesOrderLineItemDto>, SalesOrderLineItemDtoValidator>();
            services.AddScoped<IValidator<SalesOrderLineItemDetailsDto>, SalesOrderLineItemDetailsDtoValidator>();
            services.AddScoped<IValidator<SalesOrderLineItemListDto>, SalesOrderLineItemListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SalesOrderLineItemMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetSalesOrderLineItemByIdQuery>();
            });

            return services;
        }
    }
}