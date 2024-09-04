using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Back.Models
{
    public class Tutor
    {
        [Key]
        [Display(Name = "Id del tutor")]
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
        [MaxLength(20)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(70)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Relacion Relacion { get; set; }
    }

    public enum Relacion
    {
        Madre,
        Padre,
        Tutor,
        Otro
    }
}