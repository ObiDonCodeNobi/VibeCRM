using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.ProductGroup.Commands.CreateProductGroup;
using VibeCRM.Application.Features.ProductGroup.Commands.DeleteProductGroup;
using VibeCRM.Application.Features.ProductGroup.Commands.UpdateProductGroup;
using VibeCRM.Application.Features.ProductGroup.DTOs;
using VibeCRM.Application.Features.ProductGroup.Mappings;
using VibeCRM.Application.Features.ProductGroup.Queries.GetAllProductGroups;
using VibeCRM.Application.Features.ProductGroup.Queries.GetProductGroupById;
using VibeCRM.Application.Features.ProductGroup.Validators;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering ProductGroup feature services in the dependency injection container.
    /// </summary>
    public static class ProductGroupFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the ProductGroup feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all ProductGroup feature services added.</returns>
        public static IServiceCollection AddProductGroupFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateProductGroupCommand>, CreateProductGroupCommandValidator>();
            services.AddScoped<IValidator<UpdateProductGroupCommand>, UpdateProductGroupCommandValidator>();
            services.AddScoped<IValidator<DeleteProductGroupCommand>, DeleteProductGroupCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetProductGroupByIdQuery>, GetProductGroupByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllProductGroupsQuery>, GetAllProductGroupsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ProductGroupDto>, ProductGroupDtoValidator>();
            services.AddScoped<IValidator<ProductGroupDetailsDto>, ProductGroupDetailsDtoValidator>();
            services.AddScoped<IValidator<ProductGroupListDto>, ProductGroupListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductGroupMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetProductGroupByIdQuery>();
            });

            return services;
        }
    }
}