using Back.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class TipoDeVacunasController : ControllerBase
	{
		private readonly DataContext contexto;

		public TipoDeVacunasController(DataContext contexto)
		{
			this.contexto = contexto;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TipoDeVacuna>>> Get()
		{
			try
			{
				var tipos = await contexto.TipoDeVacuna.ToListAsync();
				return Ok(tipos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}