using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models
{
    public class Laboratorio
    {
        [Key]
        [Display(Name = "Id del laboratorio")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "País")]
        public string Pais { get; set; }

        [Required]
        [MaxLength(70)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [Phone]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required]
        public bool Estado { get; set; }

        public override string ToString()
        {
            return $"{Nombre}, ({Pais})";
        }
    }
}
