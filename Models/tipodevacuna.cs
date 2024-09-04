using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models
{
    public class TipoDeVacuna
    {
        [Key]
        [Display(Name = "Id del tipo de vacuna")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(255)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}