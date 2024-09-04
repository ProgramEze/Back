using Back.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class PacientesController : ControllerBase
	{
		private readonly DataContext contexto;

		public PacientesController(DataContext contexto)
		{
			this.contexto = contexto;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Paciente>>> Get()
		{
			try
			{
				var pacientes = await contexto.Paciente.ToListAsync();
				return Ok(pacientes);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{dni}")]
		public async Task<ActionResult<Paciente>> ObtenerPaciente(int dni)
		{
			try
			{
				var paciente = await contexto.Paciente.FirstOrDefaultAsync(p => p.DNI == dni.ToString());
				if (paciente == null)
				{
					return NotFound($"No se encontró ningún paciente con el DNI {dni}");
				}
				return Ok(paciente);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] Paciente paciente)
		{
			try
			{
				if (ModelState.IsValid)
				{
					contexto.Paciente.Add(paciente);
					await contexto.SaveChangesAsync();
					return CreatedAtAction(nameof(Get), new { id = paciente.Id }, paciente);
				}

				return BadRequest(ModelState);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}