using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.ProductType.Commands.CreateProductType;
using VibeCRM.Application.Features.ProductType.Commands.DeleteProductType;
using VibeCRM.Application.Features.ProductType.Commands.UpdateProductType;
using VibeCRM.Application.Features.ProductType.Mappings;
using VibeCRM.Application.Features.ProductType.Queries.GetProductTypeById;
using VibeCRM.Application.Features.ProductType.Queries.GetAllProductTypes;
using VibeCRM.Application.Features.ProductType.Validators;
using VibeCRM.Shared.DTOs.ProductType;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering ProductType feature services in the dependency injection container.
    /// </summary>
    public static class ProductTypeFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the ProductType feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all ProductType feature services added.</returns>
        public static IServiceCollection AddProductTypeFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateProductTypeCommand>, CreateProductTypeCommandValidator>();
            services.AddScoped<IValidator<UpdateProductTypeCommand>, UpdateProductTypeCommandValidator>();
            services.AddScoped<IValidator<DeleteProductTypeCommand>, DeleteProductTypeCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetProductTypeByIdQuery>, GetProductTypeByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllProductTypesQuery>, GetAllProductTypesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<ProductTypeDto>, ProductTypeDtoValidator>();
            services.AddScoped<IValidator<ProductTypeDetailsDto>, ProductTypeDetailsDtoValidator>();
            services.AddScoped<IValidator<ProductTypeListDto>, ProductTypeListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductTypeMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetProductTypeByIdQuery>();
            });
            
            return services;
        }
    }
}
