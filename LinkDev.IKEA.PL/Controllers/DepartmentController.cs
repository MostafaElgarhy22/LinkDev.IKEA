using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritence : DepartmentController is a Controller
    // Composition : DepartmentController has a IDepartmentService
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        #endregion

        #region Index
        [HttpGet] //Get: /Department/Index
        public IActionResult Index()
        {

            var departments = _departmentService.GetDepartments();

            var departmentViewModels = departments.Select(d => new DepartmentViewModel()
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
                CreationDate = d.CreationDate
            });

            return View(departmentViewModels);
        }
        #endregion

        #region Details
        [HttpGet] //Get: /Department/Details/id
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest(); //400

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound(); //404

            var departmentViewModel = new DepartmentsDetailsViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description ?? "",
                CreationDate = department.CreationDate,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn
            };


            return View(departmentViewModel);
        }
        #endregion
    }
}
