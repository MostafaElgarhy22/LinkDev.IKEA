﻿using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Persistence.Data;
using LinkDev.IKEA.DAL.Persistence.Data.DbInitializer;
using LinkDev.IKEA.DAL.Persistence.UnitOfWork;
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

            services.AddScoped<IDbInitializer,DbInitializer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        
            
    }
}
