﻿using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentsDetailsViewModel
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }

        #region Audit Fields

        [Display(Name = "Created By")]
        public required string CreatedBy { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Last Modified By")]
        public required string LastModifiedBy { get; set; }
        [Display(Name = "Last Modified On")]
        public DateTime LastModifiedOn { get; set; }
        #endregion
    }
}
