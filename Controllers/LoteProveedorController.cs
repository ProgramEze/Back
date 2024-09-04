using Microsoft.EntityFrameworkCore;
using Back.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class LoteProveedoresController : ControllerBase
	{
		private readonly DataContext contexto;
		private readonly IWebHostEnvironment environment;

		public LoteProveedoresController(DataContext contexto, IWebHostEnvironment environment)
		{
			this.contexto = contexto;
			this.environment = environment;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(contexto.LoteProveedor
				.Include(l => l.Laboratorio)
				.Include(t => t.TipoDeVacuna));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}