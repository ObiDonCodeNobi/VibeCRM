using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VibeCRM.Application.Features.Invoice.Commands.CreateInvoice;
using VibeCRM.Application.Features.Invoice.Commands.DeleteInvoice;
using VibeCRM.Application.Features.Invoice.Commands.UpdateInvoice;
using VibeCRM.Application.Features.Invoice.Mappings;
using VibeCRM.Application.Features.Invoice.Queries.GetAllInvoices;
using VibeCRM.Application.Features.Invoice.Queries.GetInvoiceById;
using VibeCRM.Application.Features.Invoice.Validators;
using VibeCRM.Application.Features.InvoiceLineItem;
using VibeCRM.Shared.DTOs.Invoice;
using VibeCRM.Shared.DTOs.InvoiceLineItem;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering Invoice feature services in the dependency injection container.
    /// </summary>
    public static class InvoiceFeatureExtensions
    {
        /// <summary>
        /// Registers all services required by the Invoice feature.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all Invoice feature services added.</returns>
        public static IServiceCollection AddInvoiceFeature(this IServiceCollection services)
        {
            // Register command validators
            services.AddScoped<IValidator<CreateInvoiceCommand>, CreateInvoiceCommandValidator>();
            services.AddScoped<IValidator<UpdateInvoiceCommand>, UpdateInvoiceCommandValidator>();
            services.AddScoped<IValidator<DeleteInvoiceCommand>, DeleteInvoiceCommandValidator>();

            // Register query validators
            services.AddScoped<IValidator<GetInvoiceByIdQuery>, GetInvoiceByIdQueryValidator>();
            services.AddScoped<IValidator<GetAllInvoicesQuery>, GetAllInvoicesQueryValidator>();

            // Register DTO validators
            services.AddScoped<IValidator<InvoiceDto>, InvoiceDtoValidator>();
            services.AddScoped<IValidator<InvoiceDetailsDto>, InvoiceDetailsDtoValidator>();
            services.AddScoped<IValidator<InvoiceLineItemDto>, InvoiceLineItemDtoValidator>();

            // Register mapping profiles
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new InvoiceMappingProfile());
            }).CreateMapper());

            return services;
        }
    }
}