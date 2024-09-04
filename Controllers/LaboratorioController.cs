using Back.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class LaboratoriosController : ControllerBase
	{
		private readonly DataContext contexto;

		public LaboratoriosController(DataContext contexto)
		{
			this.contexto = contexto;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Laboratorio>>> Get()
		{
			try
			{
				var laboratorios = await contexto.Laboratorio.ToListAsync();
				return Ok(laboratorios);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}