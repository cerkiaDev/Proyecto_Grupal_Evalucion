namespace Proyecto_Grupal.Models
{
    public class DeptEmp
    {
        public int EmpNo { get; set; }
        public string DeptNo { get; set; } = string.Empty;
        public System.DateTime FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
    }
}
