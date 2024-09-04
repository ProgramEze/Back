using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models
{
    public class Turno
    {
        [Key]
        [Display(Name = "Id del turno")]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int TipoDeVacunaId { get; set; }

        [Required]
        public int TutorId { get; set; }

        [Required]
        public int AgenteId { get; set; }

        [Required]
        public int? AplicacionId { get; set; }

        [Required]
        public DateTime Cita { get; set; }
        
        [ForeignKey(nameof(PacienteId))]
        public Paciente? Paciente { get; set; }

        [ForeignKey(nameof(TipoDeVacunaId))]
        public TipoDeVacuna? TipoDeVacuna { get; set; }

        [ForeignKey(nameof(TutorId))]
        public Tutor? Tutor { get; set; }

        [ForeignKey(nameof(AgenteId))]
        public Agente? Agente { get; set; }

        [ForeignKey(nameof(AplicacionId))]
        public Aplicacion? Aplicacion { get; set; }  
    }
}

