using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Proyecto_Grupal.Models
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string EmpNo { get; set; }

        [Required]
        [StringLength(50)]
        public string CI { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Correo { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<EmpleadoDepartamento> EmpleadoDepartamentos { get; set; } = new List<EmpleadoDepartamento>();

        public virtual ICollection<DeptManager> DeptManagers { get; set; } = new List<DeptManager>();
    }
}
