using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUDDemo.Models
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        // public Nullable<int> mgr { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        // public Nullable<decimal> comm { get; set; }
        // public Nullable<int> dept { get; set; }
    }
}