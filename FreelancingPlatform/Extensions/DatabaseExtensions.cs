using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FreelancingPlatform.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddCustomDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure())
            );
            return services;
        }

        public static WebApplication ApplyDatabaseMigrations(this WebApplication app)
        {
            if (app.Configuration.GetSection("Database").GetValue<bool>("ApplyAutomaticMigrations"))
            {
                using (var serviceScope = app.Services.CreateScope())
                {
                    var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                    appDbContext.Database.Migrate();
                }
            }
            return app;
        }
    }
}
