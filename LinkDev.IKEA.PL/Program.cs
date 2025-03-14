using LinkDev.IKEA.BLL;
using LinkDev.IKEA.DAL;
using LinkDev.IKEA.PL.Extensions;
namespace LinkDev.IKEA.PL
{
    public class Program
    {

    

        //Entry point of the application.
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddApplicationServices();
            #endregion

            var app = builder.Build();

            #region Database Initialization
            app.InitializeDatabase(); 
            #endregion

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
