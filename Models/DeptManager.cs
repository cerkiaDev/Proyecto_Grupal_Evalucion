using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupal.Models
{
    public class DeptManager
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        [Required]
        [ForeignKey("Departamento")]
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
