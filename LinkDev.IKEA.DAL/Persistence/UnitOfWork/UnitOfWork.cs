using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistence.Data;
using LinkDev.IKEA.DAL.Persistence.Repositories;

namespace LinkDev.IKEA.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IDepartmentRepository? DepartmentRepository { get; set; }


        public UnitOfWork(ApplicationDbContext dbContext) // Ask RunTime for an Instance of ApplicationDbContext
        {
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
