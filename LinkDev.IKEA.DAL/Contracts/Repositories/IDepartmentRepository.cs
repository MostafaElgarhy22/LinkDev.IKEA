using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Entities.Departments;

namespace LinkDev.IKEA.DAL.Contracts.Repositories
{
    public interface IDepartmentRepository
    {


        IEnumerable<Department> GetAll(bool withTracking = false);

        Department Get(int id);

        int Add(Department entity);

        int Update(Department entity);

        int Delete(int id);
    }
}
