using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Product.Commands.CreateProduct;
using VibeCRM.Application.Features.Product.Commands.DeleteProduct;
using VibeCRM.Application.Features.Product.Commands.UpdateProduct;
using VibeCRM.Application.Features.Product.Mappings;
using VibeCRM.Application.Features.Product.Queries.GetAllProducts;
using VibeCRM.Application.Features.Product.Queries.GetProductById;
using VibeCRM.Application.Features.Product.Validators;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Product feature services in the dependency injection container.
    /// </summary>
    public static class ProductFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Product feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Product feature services added.</returns>
        public static IServiceCollection AddProductFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            services.AddScoped<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetProductByIdQuery>, GetProductByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllProductsQuery>, GetAllProductsQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();
            services.AddScoped<IValidator<ProductDetailsDto>, ProductDetailsDtoValidator>();
            services.AddScoped<IValidator<ProductListDto>, ProductListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetProductByIdQuery>();
            });

            return services;
        }
    }
}