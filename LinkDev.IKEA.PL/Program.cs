using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Entry point of the application.
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(optionsbuilder =>
            {
                optionsbuilder.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"));

            } //, contextlLifeTime: serviceLifetime.Scoped, optionsLifetime: ServiceLifeTime.Scoped
            );
        
            builder.Services.AddScoped<ApplicationDbContext>((ServiceProvider) =>
            {

                var optionsbuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsbuilder.UseSqlServer("Server=.;Database=IKEA;Trusted_Connection=True;");

                return new ApplicationDbContext(optionsbuilder.Options);
            });


            #endregion

            var app = builder.Build();

            #region Configure HTTP Request Pipelines

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // app.UseStaticFiles();

            app.UseRouting();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets(); 
            #endregion

            app.Run();
        }
    }
}
