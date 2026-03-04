using System;

namespace Proyecto_Grupal.Models
{
    public class DeptEmp
    {
        public int EmployeeId { get; set; }
        public int DeptId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
