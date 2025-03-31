using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.InvoiceStatus.Commands.CreateInvoiceStatus;
using VibeCRM.Application.Features.InvoiceStatus.Commands.DeleteInvoiceStatus;
using VibeCRM.Application.Features.InvoiceStatus.Commands.UpdateInvoiceStatus;
using VibeCRM.Application.Features.InvoiceStatus.Mappings;
using VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusById;
using VibeCRM.Application.Features.InvoiceStatus.Queries.GetAllInvoiceStatuses;
using VibeCRM.Application.Features.InvoiceStatus.Validators;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering InvoiceStatus feature services in the dependency injection container.
    /// </summary>
    public static class InvoiceStatusFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the InvoiceStatus feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all InvoiceStatus feature services added.</returns>
        public static IServiceCollection AddInvoiceStatusFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateInvoiceStatusCommand>, CreateInvoiceStatusCommandValidator>();
            services.AddScoped<IValidator<UpdateInvoiceStatusCommand>, UpdateInvoiceStatusCommandValidator>();
            services.AddScoped<IValidator<DeleteInvoiceStatusCommand>, DeleteInvoiceStatusCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetInvoiceStatusByIdQuery>, GetInvoiceStatusByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllInvoiceStatusesQuery>, GetAllInvoiceStatusesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<InvoiceStatusDto>, InvoiceStatusDtoValidator>();
            services.AddScoped<IValidator<InvoiceStatusDetailsDto>, InvoiceStatusDetailsDtoValidator>();
            services.AddScoped<IValidator<InvoiceStatusListDto>, InvoiceStatusListDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new InvoiceStatusMappingProfile());
            }).CreateMapper());

            // Register MediatR handlers
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<GetInvoiceStatusByIdQuery>();
            });
            
            return services;
        }
    }
}
