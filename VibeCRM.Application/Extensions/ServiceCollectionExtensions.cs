using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VibeCRM.Application.DependencyInjection;

namespace VibeCRM.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for registering application services in the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all application services required by the VibeCRM system.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <returns>The service collection with all application services added.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR for all handlers in the application assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register feature-specific services
            services.AddAccountStatusFeature();
            services.AddAccountTypeFeature();
            services.AddActivityDefinitionFeature();
            services.AddActivityFeature();
            services.AddAddressFeature();
            services.AddAttachmentFeature();
            services.AddCallFeature();
            services.AddCompanyFeature();
            services.AddEmailAddressFeature();
            services.AddInvoiceFeature();
            services.AddNoteFeature();
            services.AddPaymentFeature();
            services.AddPaymentLineItemFeature();
            services.AddPersonFeature();
            services.AddPhoneFeature();
            services.AddProductFeature();
            services.AddProductGroupFeature();
            services.AddQuoteFeature();
            services.AddQuoteLineItemFeature();
            services.AddRoleFeature();
            services.AddSalesOrderFeature();
            services.AddSalesOrderLineItemFeature();
            services.AddServiceFeature();
            services.AddTeamFeature();
            services.AddUserFeature();
            services.AddWorkflowFeature();

            // Note: Additional feature registrations will be added as they are implemented

            return services;
        }
    }
}