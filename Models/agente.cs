using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models
{
    public class Agente
    {
        [Key]
        [Display(Name = "Matricula del agente")]
        public int Matricula { get; set; }

        [Required]
        [MaxLength(255)]
        public string Clave { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool Estado { get; set; }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre}";
        }
    }
}