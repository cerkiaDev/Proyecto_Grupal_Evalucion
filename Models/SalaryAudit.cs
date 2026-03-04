using System;

namespace Proyecto_Grupal.Models
{
    public class SalaryAudit
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal OldSalary { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangedBy { get; set; }
    }
}
