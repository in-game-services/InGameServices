using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InGameServices.Application.Helpers.Abstractions;
using InGameServices.Application.Helpers;
using InGameServices.Application.Services;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Application.Validators;
using InGameServices.Application.Services.Abstractions;

namespace InGameServices.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceValidator, ServiceValidator>();
            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<IServiceRatingValidator, ServiceRatingValidator>();

            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IMailSender, MailSender>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordRecoveryService, PasswordRecoveryService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IServiceRatingService, ServiceRatingService>();

            return services;
        }
    }
}
