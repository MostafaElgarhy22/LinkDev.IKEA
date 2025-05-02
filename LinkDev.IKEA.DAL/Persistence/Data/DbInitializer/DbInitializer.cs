using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Common;
using LinkDev.IKEA.DAL.Common.JsonConverter;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistence.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbcontext;

        public DbInitializer(ApplicationDbContext dbcontext)  // Ask RunTime for Creating an instant of ApplicationDbContext
        {
            _dbcontext = dbcontext;
        }

        public void Initialize()
        {
            if (_dbcontext.Database.GetPendingMigrations().Any())
                _dbcontext.Database.Migrate(); //Update the database to the latest migration
        }

        public void Seed()
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                Converters =
                    {
                        new DateOnlyJsonConverter(),
                        new GenderJsonConverter(),
                        new EmployeeTypeJsonConverter(),
                    }
            };

            if (_dbcontext.Departments.Any())
            {
                var departmentsData = File.ReadAllText("Persistence/Data/Seeds/departments.json");
                var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData, jsonSerializerOptions);

                if (departments?.Count > 0)
                {
                    _dbcontext.Departments.AddRange(departments);
                    _dbcontext.SaveChanges();
                }
            }

            if(!_dbcontext.Employees.Any())
            {
                var employeesData = File.ReadAllText("Persistence/Data/Seeds/employees.json");
              
                var employees = JsonSerializer.Deserialize<List<Employee>>(employeesData, jsonSerializerOptions);
                if (employees?.Count > 0)
                {
                    _dbcontext.Employees.AddRange(employees);
                    _dbcontext.SaveChanges();
                }
            }
        }
    }
}