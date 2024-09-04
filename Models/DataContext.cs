using Microsoft.EntityFrameworkCore;

namespace Back.Models
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		public DbSet<Agente> Agente { get; set; }
		public DbSet<Aplicacion> Aplicacion { get; set; }
		public DbSet<Laboratorio> Laboratorio { get; set; }
		public DbSet<LoteProveedor> LoteProveedor { get; set; }
		public DbSet<Paciente> Paciente { get; set; }
		public DbSet<TipoDeVacuna> TipoDeVacuna { get; set; }
		public DbSet<Turno> Turno { get; set; }
		public DbSet<Tutor> Tutor { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Aplicacion>()
				.Property(a => a.Estado)
				.HasConversion(
					v => v.ToString(),
					v => (Estado)Enum.Parse(typeof(Estado), v)
				);

			modelBuilder.Entity<Tutor>()
				.Property(t => t.Relacion)
				.HasConversion(
					v => v.ToString(),
					v => (Relacion)Enum.Parse(typeof(Relacion), v)
				);

			modelBuilder.Entity<Paciente>()
				.Property(p => p.Genero)
				.HasConversion(
					v => v.ToString(),
					v => (Genero)Enum.Parse(typeof(Genero), v)
				);

			base.OnModelCreating(modelBuilder);
		}
	}
}