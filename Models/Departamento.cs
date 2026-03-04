using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupal.Models
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string DeptNo { get; set; }

        [Required]
        [StringLength(100)]
        public string DeptName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
