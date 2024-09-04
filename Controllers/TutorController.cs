using Back.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
	[Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class TutoresController : ControllerBase
	{
		private readonly DataContext contexto;

		public TutoresController(DataContext contexto)
		{
			this.contexto = contexto;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Tutor>>> Get()
		{
			try
			{
				var tutores = await contexto.Tutor.ToListAsync();
				return Ok(tutores);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{dni}")]
		public async Task<ActionResult<Tutor>> ObtenerTutor(int dni)
		{
			try
			{
				var tutor = await contexto.Tutor.FirstOrDefaultAsync(t => t.DNI == dni.ToString());
				if (tutor == null)
				{
					return NotFound($"No se encontró ningún tutor con el DNI {dni}");
				}
				return Ok(tutor);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] Tutor tutor)
		{
			try
			{
				if (ModelState.IsValid)
				{
					contexto.Tutor.Add(tutor);
					await contexto.SaveChangesAsync();
					return CreatedAtAction(nameof(Get), new { id = tutor.Id }, tutor);
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