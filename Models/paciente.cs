using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models
{    
    public class Paciente
    {
        [Key]
        [Display(Name = "Id del paciente")]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string DNI { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }

        [Required]
        public DateOnly FechaDeNacimiento { get; set; }

        [Required]
        public Genero Genero { get; set; }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre}";
        }
    }

    public enum Genero
    {
        Masculino,
        Femenino,
        Otro
    }
}
