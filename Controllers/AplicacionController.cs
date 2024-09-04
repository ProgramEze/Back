using Back.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AplicacionesController : ControllerBase
	{
		private readonly DataContext contexto;
		private readonly IWebHostEnvironment environment;

		public AplicacionesController(DataContext contexto, IWebHostEnvironment environment)
		{
			this.contexto = contexto;
			this.environment = environment;
		}

		[HttpGet]
		public async Task<ActionResult<Aplicacion>> Get()
		{
			try
			{
				var matricula = int.Parse(User.FindFirstValue("Matricula"));
				var aplicaciones = await contexto.Aplicacion
				.Include(l => l.LoteProveedor)
					.ThenInclude(l => l.Laboratorio)
				.Include(l => l.LoteProveedor)
					.ThenInclude(t => t.TipoDeVacuna)
				.Include(l => l.Agente)
				.Where(x => x.Agente.Matricula == matricula)
				.ToListAsync();
				return Ok(aplicaciones);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}