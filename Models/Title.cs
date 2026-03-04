namespace Proyecto_Grupal.Models
{
    public class Title
    {
        public int EmpNo { get; set; }
        public string TitleName { get; set; } = string.Empty;
        public System.DateTime FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
    }
}
