using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SharedService.Middlewares;

namespace SharedService.DI
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string fileName,string dbConString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(config.GetConnectionString(dbConString), sqlServerOption => sqlServerOption.EnableRetryOnFailure()));

            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                                                   .WriteTo.Debug()
                                                   .WriteTo.Console()
                                                   .WriteTo.File(path: $"{fileName}-.text", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, outputTemplate: "{Timestemp:yyyy-MM-dd-HH:mm-ss.fff zzz} [{Level:u3}] {message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                                                   .CreateLogger();

            JWTAuthenticationScheme.AddJWTAuthScheme(services, config);
            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            // register middleware to block  all outsider APIs
            //app.UseMiddleware<ListenToOnlyApiGatewayMiddleware>();

            return app;
        }
    }
}
