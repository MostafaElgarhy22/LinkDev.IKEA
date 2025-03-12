using LinkDev.IKEA.DAL.Contracts;

namespace LinkDev.IKEA.PL.Extensions
{
    public static class InitializerExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var dbInitializer = services.GetRequiredService<IDbInitializer>();
            dbInitializer.Initialize();
            dbInitializer.Seed();
        }
        
    }
}
