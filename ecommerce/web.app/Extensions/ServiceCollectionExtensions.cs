using web.app.Services;

namespace web.app.Extensions
{
    /// <summary>
    /// Extension methods for registering application services
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds application services to the dependency injection container
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();

            return services;
        }

        /// <summary>
        /// Adds CORS configuration for React development and production
        /// </summary>
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReact", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            return services;
        }

        /// <summary>
        /// Adds API documentation (Swagger) configuration
        /// </summary>
        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
