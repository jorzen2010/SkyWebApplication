using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyWebApplication.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentInfo { get; set; }
        public int DepartmentParentID { get; set; }
        public bool DepartmentStatus { get; set; }
        public int DepartmentSort { get; set; }

        [NotMapped]
        public string DepartmentParentName { get; set; }
        [NotMapped]
        public List<Department> ChildDepartment { get; set; }
    }
}