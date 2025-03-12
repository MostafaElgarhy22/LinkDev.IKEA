using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistence.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private ApplicationDbContext _dbcontext;

        public DepartmentRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Department? Get(int id)
        {
            var department = _dbcontext.Departments.Find(id);
            department = _dbcontext.Find<Department>(id); // Ef Core 3.1 Feature

            //var department = _dbcontext.Departments.Local.FirstOrDefault(D => D.Id == id);

            //if (department is null)
            //{
            //    department = _dbcontext.Departments.FirstOrDefault(D => D.Id == id);
            //}
            return department;
        }

        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (!withTracking)
            {
                return _dbcontext.Departments.AsNoTracking();
            }
            return _dbcontext.Departments;
        }

        public int Add(Department entity)
        {
            _dbcontext.Departments.Add(entity);
            return _dbcontext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbcontext.Departments.Update(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(int id)
        {
            var department = _dbcontext.Departments.Find(id);

            if (department is { })
            {
                _dbcontext.Departments.Remove(department);
                return _dbcontext.SaveChanges();
            }

            return 0;
        }

    }
}
