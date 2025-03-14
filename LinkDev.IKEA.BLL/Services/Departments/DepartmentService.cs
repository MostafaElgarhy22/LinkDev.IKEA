using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Entities.Departments;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(id);
            if (department == null)
            {
                return null;
            }

            return new DepartmentDetailsDto(department.Id, department.CreatedBy, department.CreatedOn, department.LastModifiedBy, department.LastModifiedOn, department.Name, department.Code, department.Description, department.CreationDate);

        }

        public IEnumerable<DepartmentDto> GetDepartments()
        {
          var departments = _unitOfWork.DepartmentRepository.GetAll();
            foreach (var department in departments)
            {
                yield return new DepartmentDto(department.Id, department.Code, department.Name, department.CreationDate);
            }
        }

        public int CreateDepartment(CreateDepartmentDto department)
        {
            var departmentToCreate = new Department()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = "",
                LastModifiedBy = "",
            };
            _unitOfWork.DepartmentRepository.Add(departmentToCreate);
           return _unitOfWork.Complete();

        }

        public int UpdateDepartment(UpdateDepartmentDto department)
        {
            var departmentToUpdate = new Department()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = "",
                LastModifiedBy = "",
            };
            _unitOfWork.DepartmentRepository.Update(departmentToUpdate);
            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            _unitOfWork.DepartmentRepository.Delete(id);
            var deleted = _unitOfWork.Complete() > 0;

            return deleted;
        }

    }
}
