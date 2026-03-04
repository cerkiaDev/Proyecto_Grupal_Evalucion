using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupal.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Email { get; set; }

        // Hashed password (store result of a password hasher)
        public string PasswordHash { get; set; }
    }
}