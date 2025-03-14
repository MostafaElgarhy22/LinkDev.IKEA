﻿using LinkDev.IKEA.BLL.Models.Departments;
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
        private readonly ILogger<DepartmentController> _logger;



        public DepartmentController(ILogger <DepartmentController> logger, IDepartmentService departmentService)
        {
            _logger = logger;
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

        #region Create

        [HttpGet] //Get: /Department/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentViewModel model)
        {
            var message = "Department created successfully";
            try
            {
                if (!ModelState.IsValid) //Server side validation
                    return View(model);

       
                var departmentToCreate = new CreateDepartmentDto(model.Code, model.Name, model.Description, model.CreationDate);

                var created = _departmentService.CreateDepartment(departmentToCreate) > 0;

                if (created)
                    message = "Failed to create department";
            }
            catch (Exception ex)
            {
                // 1. Log Exception in Database or External File (By Serialog Package)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                // 2. Set Message
                message = "An error occurred while creating the department";

            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
