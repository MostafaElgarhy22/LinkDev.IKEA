using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.IKEA.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services ,IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(optionsbuilder =>
            {
                optionsbuilder.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"));

            } //, contextlLifeTime: serviceLifetime.Scoped, optionsLifetime: ServiceLifeTime.Scoped
            );

            return services;
        }
        
            
    }
}
