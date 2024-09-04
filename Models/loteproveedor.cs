using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models
{
    public class LoteProveedor
    {
        [Key]
        [Display(Name = "Id del lote")]
        public int Id { get; set; }

        [Required]
        public int NumeroDeLote { get; set; }

        [Required]
        public int LaboratorioId { get; set; }

        [Required]
        public int TipoDeVacunaId { get; set; }
        
        [Required]
        public int CantidadDeVacunas { get; set; }

        [Required]
        public DateOnly FechaDeVencimiento { get; set; }
        
        [ForeignKey(nameof(LaboratorioId))]
        public Laboratorio? Laboratorio { get; set; }
        
        [ForeignKey(nameof(TipoDeVacunaId))]
        public TipoDeVacuna? TipoDeVacuna { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}
