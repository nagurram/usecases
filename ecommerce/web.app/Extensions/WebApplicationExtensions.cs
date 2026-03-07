namespace web.app.Extensions
{
    /// <summary>
    /// Extension methods for configuring the HTTP request pipeline
    /// </summary>
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Configures API documentation (Swagger) in the HTTP request pipeline
        /// </summary>
        public static IApplicationBuilder UseApiDocumentation(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }

        /// <summary>
        /// Configures BFF (Backend-For-Frontend) pattern:
        /// - Serves static files from wwwroot
        /// - Implements SPA routing fallback (index.html for non-API routes)
        /// - Caches static assets with proper headers
        /// </summary>
        public static IApplicationBuilder UseBFFStaticFiles(this IApplicationBuilder app)
        {
            // Configure static file options with caching
            var staticFileOptions = new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Cache static assets (JS, CSS, images) for 1 year
                    if (ctx.File.Name.EndsWith(".js") || 
                        ctx.File.Name.EndsWith(".css") || 
                        ctx.File.Name.EndsWith(".png") || 
                        ctx.File.Name.EndsWith(".jpg") || 
                        ctx.File.Name.EndsWith(".jpeg") || 
                        ctx.File.Name.EndsWith(".gif") || 
                        ctx.File.Name.EndsWith(".svg") || 
                        ctx.File.Name.EndsWith(".woff") || 
                        ctx.File.Name.EndsWith(".woff2"))
                    {
                        ctx.Context.Response.Headers.CacheControl = "public, max-age=31536000"; // 1 year
                    }
                    else
                    {
                        // Don't cache HTML files
                        ctx.Context.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
                    }
                }
            };

            // Serve static files from wwwroot
            app.UseDefaultFiles();
            app.UseStaticFiles(staticFileOptions);

            // SPA fallback: route non-API requests to index.html for client-side routing
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value;

                // Skip API and health check endpoints
                if (path != null && !path.StartsWith("/api") && !path.StartsWith("/swagger"))
                {
                    // Check if the requested file exists
                    var fileInfo = app.ApplicationServices
                        .GetRequiredService<IWebHostEnvironment>()
                        .WebRootFileProvider
                        .GetFileInfo(path);

                    if (!fileInfo.Exists)
                    {
                        // Rewrite to index.html for SPA routing
                        context.Request.Path = "/index.html";
                    }
                }

                await next();
            });

            return app;
        }

        /// <summary>
        /// Maps all application endpoints (Controllers, Health checks, etc.)
        /// </summary>
        public static WebApplication MapApplicationEndpoints(this WebApplication app)
        {
            // API Controllers
            app.MapControllers();

            // Health check endpoint
            app.MapGet("/api/health", () => new { status = "healthy", timestamp = DateTime.UtcNow })
                .WithName("Health");

            return app;
        }
    }
}
